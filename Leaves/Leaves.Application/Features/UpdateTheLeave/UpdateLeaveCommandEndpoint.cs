using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.ServiceResult.Extensions;

namespace Leaves.Leaves.Application.Features.UpdateTheLeave;

public static class UpdateTheLeaveEndpoint
{
    public static RouteGroupBuilder AddUpdateTheLeaveEndpoint(this RouteGroupBuilder builder)
    {
        builder.MapPut("/UpdateTheLeave/{leaveId:int}",
            async (
                int leaveId,
                [FromBody] UpdateTheLeaveRequest request,
                IMediator mediator) =>
            {
                var command = new UpdateTheLeaveCommand(
                    leaveId,
                    request.StartedDate,
                    request.EndedDate,
                    request.Status
                );

                var response = await mediator.Send(command);

                return response.ToResult();
            });

        return builder;
    }
}