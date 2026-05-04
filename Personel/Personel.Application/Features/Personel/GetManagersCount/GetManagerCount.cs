using MediatR;
using Shared.ServiceResult;

namespace Personel.Personel.Application.Features.Personel.GetManagersCount;

public record GetManagerCount:IRequest<ServiceResult<int>>{}