using MediatR;
using Shared.ServiceResult.Extensions;

namespace Leaves.Leaves.Application.Features.GetLeavesForApproval;

public static class GetLeavesEndpoint
{
    public static RouteGroupBuilder AddGetLeavesForApproval(this RouteGroupBuilder builder)
    {
        builder.MapGet("/GetLeaveListForApproval/{personelId:int}",
            async (int personelId, IMediator mediator) =>
            {
                var query = new GetLeavesForApprovalQuery(personelId);
                var response = await mediator.Send(query);
                return response.ToResult();
            });
        return builder;
    }
}

