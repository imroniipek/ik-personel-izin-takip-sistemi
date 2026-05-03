using MediatR;
using Shared.ServiceResult;

namespace Approval.Approval.Application.Features.GetAllPersonelPendingListByManagerId;

public record GetAllPersonelPendingListByManagerId(int ManagerId):IRequest<ServiceResult<List<GetAllPersonelPendingListByManagerIdDto>>>{};