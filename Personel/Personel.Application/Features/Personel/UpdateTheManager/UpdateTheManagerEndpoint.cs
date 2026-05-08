using MediatR;
using Shared.ServiceResult.Extensions;

namespace Personel.Personel.Application.Features.Personel.UpdateTheManager;

public static class UpdateTheManagerEndpoint
{
    public static RouteGroupBuilder AddUpdateTheManagerEndpoint(this RouteGroupBuilder builder)
    {

        builder.MapPut("/UpdateTheManager", async (int managerId, int departmentId, IMediator mediator) =>
        {
            var theResponse=await mediator.Send(new UpdateTheManagerCommand(managerId, departmentId));
            return theResponse.ToResult();
        });

        return builder;
    }
}