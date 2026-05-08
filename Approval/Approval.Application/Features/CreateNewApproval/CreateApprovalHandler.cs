using System.Net;
using Approval.Approval.Application.Abstraction.Client;
using Approval.Approval.Application.Abstraction.Repository;
using MediatR;
using Shared.Dtos;
using Shared.ServiceResult;

namespace Approval.Approval.Application.Features.CreateNewApproval;

public class CreateApprovalHandler(
    IGetPersonelByManagerIdFromServices managerIdFromServices,
    IGetLeaveListForApproval leaveListForApproval,
    IApprovalRepository repository,
    IPutLeaveAfterApproval putLeaveAfterApproval
) : IRequestHandler<CreateApproval, ServiceResult<CreateApprovalResponse>>
{
    public async Task<ServiceResult<CreateApprovalResponse>> Handle(
        CreateApproval request,
        CancellationToken cancellationToken)
    {
        var personelList = await managerIdFromServices.GetPersonelByManagerId(request.ManagerId);

        if (!personelList.Any())
        {
            return ServiceResult<CreateApprovalResponse>.Error(
                "Personeller bulunamadı",
                "Bu manager'a bağlı personel bulunamadı.",
                HttpStatusCode.NotFound
            );
        }

        var selectedPersonel = personelList
            .FirstOrDefault(x => x.PersonelId == request.PersonelId);
        if (selectedPersonel is null)
        {
            return ServiceResult<CreateApprovalResponse>.Error(
                "Personel bulunamadı",
                $"{request.PersonelId} id'li personel bu manager'a bağlı değil.",
                HttpStatusCode.NotFound
            );
        }

        var leaveList =
            await leaveListForApproval.GetLeaveListForApproval(request.PersonelId);

        if (!leaveList.Any())
        {
            return ServiceResult<CreateApprovalResponse>.Error(
                "İzin bulunamadı",
                $"{request.PersonelId} personele ait onay bekleyen izin bulunamadı.",
                HttpStatusCode.NotFound
            );
        }

        var selectedLeave = leaveList
            .FirstOrDefault(x =>
                x.Id == request.LeaveId &&
                x.PersonelId == request.PersonelId
            );

        if (selectedLeave is null)
        {
            return ServiceResult<CreateApprovalResponse>.Error(
                "İzin bulunamadı",
                $"{request.PersonelId} personele ait {request.LeaveId} id'li uygun izin bulunamadı.",
                HttpStatusCode.NotFound
            );
        }

        var newApproval = new Domain.Approval
        {
            LeaveId = selectedLeave.Id,
            PersonelId = request.PersonelId,
            ManagerId = request.ManagerId,
            Status = request.Status,
            CreatedAt = DateOnly.FromDateTime(DateTime.Now),
            RejectionReason = request.RejectReason
        };

        await repository.CreateApproval(newApproval);

        var updatedLeaveDto = new LeaveDto(
            selectedLeave.Id,
            selectedLeave.PersonelId,
            request.Status,
            selectedLeave.StartedDate,
            selectedLeave.EndedDate
        );

        await putLeaveAfterApproval.UpdateTheLeave(
            selectedLeave.Id,
            updatedLeaveDto
        );

        var createApprovalResponse = new CreateApprovalResponse(
            request.PersonelId,
            selectedLeave.Id,
            request.Status
        );

        return ServiceResult<CreateApprovalResponse>.SuccessCreatedOk(
            createApprovalResponse,
            $"/api/approvals/"
        );
    }
}

