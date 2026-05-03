using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Shared.ServiceResult.Extensions;

namespace Personel.Personel.Application.Features.Personel.CreatePersonel;

public static class CreatePersonelEndpoint
{
    public static RouteGroupBuilder CreateNewPersonelEndpoint(this RouteGroupBuilder builder)
    {
        builder.MapPost("/CreateNewPersonel",
            async ([FromBody] CreatePersonelCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return result.ToResult();
            });

        return builder;
    }


}