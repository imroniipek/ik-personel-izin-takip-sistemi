
using Approval.Approval.Application.Features.GetAllPersonelPendingListByManagerId;
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