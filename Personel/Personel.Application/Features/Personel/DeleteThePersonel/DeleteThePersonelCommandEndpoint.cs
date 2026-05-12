using MediatR;
using Shared.ServiceResult.Extensions;

namespace Personel.Personel.Application.Features.Personel.DeleteThePersonel;

public static class DeleteThePersonelCommandEndpoint
{

    public static RouteGroupBuilder AddDeleteThePersonelCommandEndpoint(this RouteGroupBuilder builder)
    {
        builder.MapDelete("/DeleteThePersonel/{personelId}",   async (int personelId, IMediator mediator)=>
        {
            var command = new DeleteThePersonelCommand(personelId);

            var result = await mediator.Send(command);

            return result.ToResult();
        });
        return builder;
    }
}