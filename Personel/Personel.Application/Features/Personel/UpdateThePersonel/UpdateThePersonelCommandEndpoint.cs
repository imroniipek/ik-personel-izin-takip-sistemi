using MediatR;
using Shared.ServiceResult.Extensions;

namespace Personel.Personel.Application.Features.Personel.UpdateThePersonel;

public static class UpdateThePersonelCommandEndpoint
{
    public static RouteGroupBuilder AddUpdateThePersonelCommandEndpoint(this RouteGroupBuilder builder)
    {
        builder.MapPut("/UpdateThePersonel/{personelId:int}",
            async (int personelId, Domain.Personel personel, IMediator mediator) =>
            {
                var theResponse = await mediator.Send(
                    new UpdateThePersonelCommand(personelId, personel)
                );

                return theResponse.ToResult();
            });
        return builder;
    }
}