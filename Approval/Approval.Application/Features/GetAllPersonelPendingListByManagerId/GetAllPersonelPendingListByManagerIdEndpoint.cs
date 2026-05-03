using MediatR;
using Shared.ServiceResult.Extensions;

namespace Approval.Approval.Application.Features.GetAllPersonelPendingListByManagerId;

public static class GetAllPersonelPendingListByManagerIdEndpoint
{
    public static RouteGroupBuilder AddGetAllPersonelPendingListByManagerIdEndpoint(this RouteGroupBuilder builder)
    {
        builder.MapGet("/GetPendingLeavesListForApprovalByPersonelIdQuery/{managerId}",
            async (int managerId, IMediator mediator) =>
            {
                var response = await mediator.Send(
                    new GetAllPersonelPendingListByManagerId(managerId)
                );

                return response.ToResult();
            });

        return builder;
    }
}