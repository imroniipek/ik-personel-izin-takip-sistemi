using MediatR;
using Shared.ServiceResult.Extensions;

namespace Personel.Personel.Application.Features.Personel.DeleteTheManager;

public static class DeleteTheManagerEndpoint
{
    public static RouteGroupBuilder AddDeleteTheManagerEndpoint(this RouteGroupBuilder builder)
    {
        builder.MapDelete("/DeleteTheManager/{departmentId:int}", async (int departmentId,
            IMediator mediator) =>
        {
            var theResponse = await mediator.Send(
                new DeleteTheManagerCommand(departmentId)
            );

            return theResponse.ToResult();
        });

        return builder;
    }
}