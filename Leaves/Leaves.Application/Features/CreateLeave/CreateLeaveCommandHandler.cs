using System.Net;
using Leaves.Leaves.Application.Abstraction.Clients;
using Leaves.Leaves.Application.Abstraction.Repositories;
using MediatR;
using Shared;
using Shared.ServiceResult;

namespace Leaves.Leaves.Application.Features.CreateLeave;

public class CreateLeaveCommandHandler(
    ILeaveRepository repository,
    IPersonelApi personelApi
) : IRequestHandler<CreateLeaveCommand, ServiceResult<CreateLeaveResponse>>
{
    public async Task<ServiceResult<CreateLeaveResponse>> Handle(CreateLeaveCommand request, CancellationToken cancellationToken)
    {
        if (request.EndedDate < request.StartedDate)
        {
            return ServiceResult<CreateLeaveResponse>.Error(
                "Invalid date range",
                "Bitiş tarihi başlangıç tarihinden önce olamaz.",
                HttpStatusCode.BadRequest
            );
        }
        
        var thePersonelHire = await personelApi.GetThePersonelHire(request.PersonelId);

        var totalEntitledDays = CalculateLeaveDays(thePersonelHire.HireDate, DateTime.UtcNow);

        if (totalEntitledDays == 0)
        {
            return ServiceResult<CreateLeaveResponse>.Error(
                "Insufficient leave entitlement",
                "Çalışanın henüz yıllık izin hakkı oluşmamış.",
                HttpStatusCode.BadRequest
            );
        }

        var usedLeaveDays = await repository.GetAllLeavesByPersonelIdForTheOneYear(request.PersonelId);

        var requestedLeaveDays = request.EndedDate.DayNumber - request.StartedDate.DayNumber+1;

        if (totalEntitledDays < usedLeaveDays + requestedLeaveDays)
        {
            var remainingDays = totalEntitledDays - usedLeaveDays;

            return ServiceResult<CreateLeaveResponse>.Error(
                "Insufficient leave entitlement",
                $"Durum-1: İzin Gün Sayısı Yetersiz: Çalışan en fazla {remainingDays} gün daha izin kullanabilir.",
                HttpStatusCode.BadRequest
            );
        }

        var pendingListCount=await repository.GetPendingLeavesCountByPersonelId(request.PersonelId);
        
        if (requestedLeaveDays + pendingListCount > totalEntitledDays - usedLeaveDays)
        {
            return ServiceResult<CreateLeaveResponse>.Error(
                "Insufficient leave entitlement",
                $"Durum-2: İzin Talep Sayısı Aşımı: İzin Talep Hakkınız Dolmuştur. Toplamda {pendingListCount} gün izin talebinde bulundunuz. Bölüm Yöneticiniz tarafından izin talepleriniz cevaplandıktan sonra tekrar deneyiniz .En fazla {totalEntitledDays - usedLeaveDays} İzin Hakkı Talebinde Bulunabilirsiniz",
                HttpStatusCode.BadRequest
            );
        }
        
        var leave = new Domain.Leave
        {
            PersonelId = request.PersonelId,
            StartedDate = request.StartedDate,
            EndedDate = request.EndedDate,
            Status = LeaveStatus.Pending
        };

        var createdLeave = await repository.AddTheLeave(leave);

        var theLeaveResponse = new CreateLeaveResponse(
            createdLeave.Id,
            createdLeave.PersonelId,
            createdLeave.StartedDate,
            createdLeave.EndedDate,
            createdLeave.Status
        );

        return ServiceResult<CreateLeaveResponse>.SuccessCreatedOk(
            theLeaveResponse,
            $"/api/leaves/{createdLeave.Id}"
        );
    }

    private int CalculateLeaveDays(DateOnly hireDate, DateTime today)
    {
        var todayDate = DateOnly.FromDateTime(today);

        int years = todayDate.Year - hireDate.Year;

        if (todayDate < hireDate.AddYears(years))
        {
            years--;
        }

        if (years < 1)
            return 0;

        if (years <= 5)
            return 14;

        return 20;
    }
}