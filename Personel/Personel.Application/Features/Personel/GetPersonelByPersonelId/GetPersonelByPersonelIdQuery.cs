using MediatR;
using Shared.ServiceResult;

namespace Personel.Personel.Application.Features.Personel.GetPersonelByPersonelId;

public record GetPersonelByPersonelIdQuery(int PersonelId):IRequest<ServiceResult<Domain.Personel>>{}