using Leaves.Leave.Infrastucture.Context;
using Leaves.Leaves.Application.Abstraction.Repositories;
using Leaves.Leaves.Application.Features.GetAcceptedLeavesByPersonelId;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Leaves.Leave.Infrastucture.Repository;

public class LeaveRepository(LeaveDbContext context) : ILeaveRepository
{
    public async Task<bool?> IsThePersonel(int personelId)
    {
        return await context.Leaves
            .AsNoTracking()
            .AnyAsync(x => x.PersonelId == personelId);
    }

    public async Task<int> GetAllLeavesByPersonelIdForTheOneYear(int personelId)
    {
        var startOfYear = new DateOnly(DateTime.Now.Year, 1, 1);
        var startOfNextYear = startOfYear.AddYears(1);

        var theLeaveList = await context.Leaves
            .AsNoTracking()
            .Where(x => x.PersonelId == personelId &&
                        x.StartedDate >= startOfYear &&
                        x.StartedDate < startOfNextYear&&x.Status==LeaveStatus.Approved)
            .ToListAsync();

        return theLeaveList.Sum(x => x.EndedDate.DayNumber - x.StartedDate.DayNumber+1);
    }

    public async Task<Leaves.Domain.Leave?> AddTheLeave(Leaves.Domain.Leave leave)
    {
        var theLeaveEntity = await context.Leaves.AddAsync(leave);
        await context.SaveChangesAsync();
        return theLeaveEntity.Entity;
    }

    public async Task<List<Leaves.Domain.Leave>?> GetLeavesNotApproved(int personelId)
    {
        return await context.Leaves
            .AsNoTracking()
            .Where(x => x.PersonelId == personelId && x.Status == LeaveStatus.Pending)
            .ToListAsync();
    }

    public async Task Update(Leaves.Domain.Leave requestLeave)
    {
        context.Leaves.Update(requestLeave);
        await context.SaveChangesAsync();
    }

    public async Task<Leaves.Domain.Leave?> FindTheLeaveByLeaveId(int leaveId)
    {
        return await context.Leaves
            .FirstOrDefaultAsync(x => x.Id == leaveId);
    }

    public async Task<int> UsedLeaveDays(int personelId)
    {
        var leaves = await context.Leaves
            .AsNoTracking()
            .Where(x => x.PersonelId == personelId &&
                        x.Status == LeaveStatus.Approved)
            .ToListAsync();

        var totalUsedDays = 0;

        foreach (var leave in leaves)
        {
            var dayCount = (leave.EndedDate.DayNumber - leave.StartedDate.DayNumber)+1 ;
            totalUsedDays += dayCount;
        }

        return totalUsedDays;
    }
    
    public async Task<LeaveListResponse> GetApprovedLeavesByPersonelId(int personelId)
    {
        var leaves = await context.Leaves
            .AsNoTracking()
            .Where(x => x.PersonelId == personelId &&
                        x.Status == LeaveStatus.Approved)
            .ToListAsync();
        
        
        var response = new LeaveListResponse(leaves.Count, leaves);

        return response;
    }

    public async Task<LeaveListResponse> GetRejectedLeavesByPersonelId(int personelId)
    {
        var leaves = await context.Leaves
            .AsNoTracking()
            .Where(x => x.PersonelId == personelId &&
                        x.Status == LeaveStatus.Rejected)
            .ToListAsync();

        return new LeaveListResponse(leaves.Count, leaves);
    }
    
    public async Task<LeaveListResponse> GetPendingLeavesByPersonelId(int personelId)
    {
        var leaves = await context.Leaves
            .AsNoTracking()
            .Where(x => x.PersonelId == personelId &&
                        x.Status == LeaveStatus.Pending)
            .ToListAsync();

        var response = new LeaveListResponse(leaves.Count, leaves);

        return response;
    }

    public async Task<List<Leaves.Domain.Leave>> GetPendingLeavesForApprovalByPersonelId(int personelId)
    {
        var leaves = await context.Leaves
            .AsNoTracking()
            .Where(x => x.PersonelId == personelId &&
                        x.Status == LeaveStatus.Pending)
            .ToListAsync();

        return leaves;

    }

    public async Task<bool> UpdateTheLeave(int leaveId, Leaves.Domain.Leave leave)
    {
        var currentLeave = await context.Leaves
            .FirstOrDefaultAsync(x => x.Id == leaveId);

        if (currentLeave is null)
            return false;

        currentLeave.StartedDate = leave.StartedDate;
        currentLeave.EndedDate = leave.EndedDate;
        currentLeave.Status = leave.Status;

        await context.SaveChangesAsync();

        return true;
    }

    public async Task<int> GetPendingLeavesCountByPersonelId(int personelId)
    {
        var leaves = await context.Leaves
            .AsNoTracking()
            .Where(x => x.PersonelId == personelId &&
                        x.Status == LeaveStatus.Pending)
            .ToListAsync();

        int totalPendingLeaveDays = 0;

        foreach (var leave in leaves)
        {
            int dayCount = leave.EndedDate.DayNumber - leave.StartedDate.DayNumber;

            totalPendingLeaveDays += dayCount;
        }
        return totalPendingLeaveDays+1;
    }
}
