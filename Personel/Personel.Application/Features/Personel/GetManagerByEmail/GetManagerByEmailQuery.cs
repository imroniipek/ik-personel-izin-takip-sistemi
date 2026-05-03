using MediatR;
using Personel.Personel.Application.Features.Personel.GetPersonelByEmail;
using Shared.ServiceResult;

namespace Personel.Personel.Application.Features.Personel.GetManagerByEmail;

public record GetManagerByEmailQuery(string Email):IRequest<ServiceResult<GetPersonelByEmailResponse>>{}