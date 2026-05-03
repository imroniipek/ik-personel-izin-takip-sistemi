using MediatR;
using Shared;
using Shared.ServiceResult;

namespace Leaves.Leaves.Application.Features.UpdateTheLeave;

public record UpdateTheLeaveCommand(
    int LeaveId,
    DateOnly StartedDate,
    DateOnly EndedDate,
    LeaveStatus Status
) : IRequest<ServiceResult>;