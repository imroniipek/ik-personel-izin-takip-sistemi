using MediatR;
using Shared.Dtos;

namespace Leaves.Leaves.Application.Features.GetPendingLeavesListForApprovalByPersonelId;

public record GetPendingLeavesListForApprovalByPersonelId(List<PersonelDto> PersonelIdDtoList ) : IRequest<List<Domain.Leave>>;