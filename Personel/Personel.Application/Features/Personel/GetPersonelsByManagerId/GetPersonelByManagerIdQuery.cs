using MediatR;
using Shared.Dtos;
using Shared.ServiceResult;

namespace Personel.Personel.Application.Features.Personel.GetPersonelsByManagerId;

public record GetPersonelByManagerIdQuery(int ManagerId)
    : IRequest<ServiceResult<List<PersonelDto>>>;