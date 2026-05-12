using Approval.Approval.Application.Abstraction.Repository;
using Approval.Approval.Infrastucture.Context;

namespace Approval.Approval.Infrastucture.Repository;

public class ApprovalRepository(ApprovalDbContext context):IApprovalRepository
{
    public async Task<Domain.Approval> CreateApproval(Domain.Approval approval)
    {

        context.Approvals.Add(approval);
        await context.SaveChangesAsync();
        return approval;
        
    }

    public async Task DeleteApproval(int id)
    {
        var entity = await context.Approvals.FindAsync(id);

        if (entity != null)
        {
            context.Approvals.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}