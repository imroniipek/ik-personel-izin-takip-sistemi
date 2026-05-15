using MediatR;
using Shared.ServiceResult.Extensions;

namespace Personel.Personel.Application.Features.Personel.GetAllPersonelsWithoutManager;

public static class GetAllPersonelsWithoutManagerQueryEndpoint
{
    public static RouteGroupBuilder AddGetPersonelsWithoutManagerQueryEndpoint(this RouteGroupBuilder builder)
    {
        builder.MapGet("/GetAllPersonelsWithoutManager", async (int managerId, int departmentId, IMediator mediator) =>
        {
            var response = await mediator.Send(
                new GetAllPersonelsWithoutManagerQuery(departmentId, managerId)
            );

            return response.ToResult();
        });

        return builder;
    }
}