using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.ServiceResult.Extensions;

namespace Personel.Personel.Application.Features.Personel.GetManagerByEmail;

public static class GetManagerByEmailQueryEndpoint
{
    public static RouteGroupBuilder AddGetManagerByEmailEndpoint(this RouteGroupBuilder builder)
    {
        builder.MapGet("/GetManagerByEmail", async ([FromQuery] string email, IMediator mediator) =>
        {
            var response = await mediator.Send(new GetManagerByEmailQuery(email));
            return response.ToResult();
        });
        return builder;
    }
}