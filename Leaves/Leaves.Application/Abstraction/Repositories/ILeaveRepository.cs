using Leaves.Leaves.Application.Features.GetAcceptedLeavesByPersonelId;

namespace Leaves.Leaves.Application.Abstraction.Repositories;

public interface ILeaveRepository
{
    Task<bool?>IsThePersonel(int personelId);

    Task<int> GetAllLeavesByPersonelIdForTheOneYear(int personelId);

    Task<Leaves.Domain.Leave?> AddTheLeave(Leaves.Domain.Leave leave);

    Task<List<Domain.Leave>?> GetLeavesNotApproved(int personelId);
    Task  Update(Domain.Leave requestLeave);

    Task<Domain.Leave?> FindTheLeaveByLeaveId(int leaveId);

    Task<int> UsedLeaveDays(int personelId);

    Task<LeaveListResponse> GetApprovedLeavesByPersonelId(int personelId);
    
    Task<LeaveListResponse> GetRejectedLeavesByPersonelId(int personelId);
    
    Task<LeaveListResponse> GetPendingLeavesByPersonelId(int personelId);

    Task<List<Domain.Leave>> GetPendingLeavesForApprovalByPersonelId(int personelId);

    Task<bool> UpdateTheLeave(int leaveId, Domain.Leave leave);
}