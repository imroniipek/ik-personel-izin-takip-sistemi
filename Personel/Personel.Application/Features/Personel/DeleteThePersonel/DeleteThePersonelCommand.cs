using MediatR;
using Shared.ServiceResult;

namespace Personel.Personel.Application.Features.Personel.DeleteThePersonel;

public record class DeleteThePersonelCommand(int PersonelId):IRequest<ServiceResult<bool>>;