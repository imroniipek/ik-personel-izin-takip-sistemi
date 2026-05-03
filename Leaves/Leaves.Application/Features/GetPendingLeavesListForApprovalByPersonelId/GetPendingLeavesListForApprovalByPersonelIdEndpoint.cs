using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;

namespace Leaves.Leaves.Application.Features.GetPendingLeavesListForApprovalByPersonelId;

public static class GetPendingLeavesListForApprovalByPersonelIdEndpoint
{
    public static RouteGroupBuilder AddGetPendingLeavesByPersonelIdEndpoint(
        this RouteGroupBuilder builder)
    {
        builder.MapPost("/GetPendingLeavesListForApprovalByPersonelIdQuery",
            async (
                [FromBody] List<PersonelDto> personelIdDtoList,
                IMediator mediator) =>
            {
                var query = new GetPendingLeavesListForApprovalByPersonelId(
                    personelIdDtoList
                );

                var response = await mediator.Send(query);

                return Results.Ok(response);
            });

        return builder;
    }
}