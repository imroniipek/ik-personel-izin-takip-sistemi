
using Approval.Approval.Application.Features.GetAllPersonelPendingListByManagerId;
using Leaves.Leaves.Application.Features.CreateLeave;

namespace Approval.Approval.Application;

public static class ApprovalExtension
{

    public static WebApplication AddAllExtension(this WebApplication app)
    {

        var group = app.MapGroup("/api");

        group.AddGetAllPersonelPendingListByManagerIdEndpoint();

        return app;
    }
}