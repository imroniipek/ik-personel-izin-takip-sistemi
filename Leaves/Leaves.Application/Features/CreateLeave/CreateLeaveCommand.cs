using MediatR;
using Shared.ServiceResult;

namespace Leaves.Leaves.Application.Features.CreateLeave;

public record CreateLeaveCommand(int PersonelId, DateOnly StartedDate, DateOnly EndedDate ) : IRequest<ServiceResult<CreateLeaveResponse>>;