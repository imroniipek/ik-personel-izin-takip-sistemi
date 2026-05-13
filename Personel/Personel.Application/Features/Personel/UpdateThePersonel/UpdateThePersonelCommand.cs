using MediatR;
using Shared.ServiceResult;

namespace Personel.Personel.Application.Features.Personel.UpdateThePersonel;

public record UpdateThePersonelCommand(int PersonelId, Domain.Personel Personel):IRequest<ServiceResult>;
