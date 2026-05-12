using MediatR;
using Shared.ServiceResult;

namespace Personel.Personel.Application.Features.Personel.GetThePersonel;

public record GetThePersonelQuery(int PersonelId) : IRequest<ServiceResult<GetThePersonelQueryResponse>>
{}