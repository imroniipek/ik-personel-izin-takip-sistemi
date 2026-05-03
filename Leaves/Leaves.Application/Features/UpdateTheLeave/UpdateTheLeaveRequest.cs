using Shared;

namespace Leaves.Leaves.Application.Features.UpdateTheLeave;

public record UpdateTheLeaveRequest(
    DateOnly StartedDate,
    DateOnly EndedDate,
    LeaveStatus Status
);