using Leaves.Leaves.Domain;
using Refit;
using Shared.Dtos;

namespace Approval.Approval.Application.Abstraction.Client;

public interface IGetPendingListByPersonelId
{
    [Post("/api/GetPendingLeavesListForApprovalByPersonelIdQuery")]
    Task<List<Leave>> GetPendingLeaveListByListPersonelIdDto([Body] List<PersonelDto> personelIds);
}