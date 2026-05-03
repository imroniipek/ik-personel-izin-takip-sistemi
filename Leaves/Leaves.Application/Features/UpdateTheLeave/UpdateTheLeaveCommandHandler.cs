using System.Net;
using Leaves.Leaves.Application.Abstraction.Repositories;
using MediatR;
using Shared.ServiceResult;

namespace Leaves.Leaves.Application.Features.UpdateTheLeave;

public class UpdateTheLeaveCommandHandler(ILeaveRepository leaveRepository ) : IRequestHandler<UpdateTheLeaveCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(
        UpdateTheLeaveCommand request,
        CancellationToken cancellationToken)
    {
        if (request.EndedDate < request.StartedDate)
        {
            return ServiceResult.Error(
                "Tarih Hatası",
                "İzin bitiş tarihi başlangıç tarihinden küçük olamaz",
                HttpStatusCode.Conflict
            );
        }

        var leave = new Domain.Leave
        {
            StartedDate = request.StartedDate,
            EndedDate = request.EndedDate,
            Status = request.Status
        };

        var isUpdated = await leaveRepository.UpdateTheLeave(
            request.LeaveId,
            leave
        );

        if (!isUpdated)
        {
            return ServiceResult.ErrorAsNoFound();
        }

        return ServiceResult.SuccessAsNoContent();
    }
}