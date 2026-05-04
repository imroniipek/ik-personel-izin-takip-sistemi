using Leaves.Leaves.Application.Abstraction.Repositories;
using MediatR;
namespace Leaves.Leaves.Application.Features.GetPendingLeavesListForApprovalByPersonelId;

public class GetPendingLeavesListForApprovalByPersonelIdHandler(ILeaveRepository repository ) : IRequestHandler<GetPendingLeavesListForApprovalByPersonelId, List<Domain.Leave>>
{
    public async Task<List<Domain.Leave>> Handle(
        GetPendingLeavesListForApprovalByPersonelId request,
        CancellationToken cancellationToken)
    {
        List<Domain.Leave> theLeaveList = new();

        foreach (var personelIdDto in request.PersonelIdDtoList)
        {
            var theList = await repository
                .GetPendingLeavesForApprovalByPersonelId(personelIdDto.PersonelId);

            theLeaveList.AddRange(theList);
        }

        return theLeaveList;
    }
}