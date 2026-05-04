using MediatR;
using Shared.ServiceResult.Extensions;

namespace Personel.Personel.Application.Features.Personel.GetManagersCount;

public static class GetManagerCountEndpoint
{
    public static RouteGroupBuilder AddGetManagerCountManager(this RouteGroupBuilder builder)
    {
        builder.MapGet("/GetTheManagerCount", async (IMediator mediator) =>
        {
            var theResponse = await mediator.Send(new GetManagerCount());

            return theResponse.ToResult();
           
        });

        return builder;
    }
}