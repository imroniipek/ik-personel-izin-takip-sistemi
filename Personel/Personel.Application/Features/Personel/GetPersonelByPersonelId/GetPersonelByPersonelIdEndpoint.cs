using MediatR;
using Shared.ServiceResult.Extensions;

namespace Personel.Personel.Application.Features.Personel.GetPersonelByPersonelId;

public static class GetPersonelByPersonelIdEndpoint
{
    public static RouteGroupBuilder AddGetPersonelIdEndpoint(this RouteGroupBuilder builder)
    {
        builder.MapGet("/GetThePersonelByPersonelId/{personelId}", async (int personelId, IMediator mediator) =>
        {
            var theResponse=await mediator.Send(new GetPersonelByPersonelIdQuery(personelId));

            return theResponse.ToResult();

        });
        return builder;
    }
}