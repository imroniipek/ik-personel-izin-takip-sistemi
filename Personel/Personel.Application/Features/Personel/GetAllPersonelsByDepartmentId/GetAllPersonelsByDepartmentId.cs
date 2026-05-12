using MediatR;
using Shared.ServiceResult;

namespace Personel.Personel.Application.Features.Personel.GetAllPersonelsByDepartmentId;

public record GetAllPersonelsByDepartmentId(int DepartmentId) : IRequest<ServiceResult<List<GetPersonelByDepartmentIdDto>>>{}