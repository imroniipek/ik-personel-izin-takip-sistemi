using MediatR;
using Personel.Personel.Application.Features.Personel.Dtos;
using Shared.ServiceResult;

namespace Personel.Personel.Application.Features.Personel.GetAllPersonelsWithoutManager;

public record GetAllPersonelsWithoutManagerQuery(
    int DepartmentId,
    int ManagerId
) : IRequest<ServiceResult<List<PersonelDto>>>;