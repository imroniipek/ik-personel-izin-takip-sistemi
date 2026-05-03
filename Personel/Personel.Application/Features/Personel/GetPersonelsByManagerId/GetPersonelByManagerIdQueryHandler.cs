using MediatR;
using Personel.Personel.Application.Abstraction;
using Shared.Dtos;
using Shared.ServiceResult;

namespace Personel.Personel.Application.Features.Personel.GetPersonelsByManagerId;

public class GetPersonelByManagerIdQueryHandler(IDepartmentRepository repository)
    : IRequestHandler<GetPersonelByManagerIdQuery, ServiceResult<List<PersonelDto>>>
{
    public async Task<ServiceResult<List<PersonelDto>>> Handle(
        GetPersonelByManagerIdQuery request,
        CancellationToken cancellationToken)
    {
        var personelList = await repository.GetPersonelsByManagerIdAsync(request.ManagerId);

        var personelDtoList = personelList
            .Select(x => new PersonelDto(x.Id,x.FirstName,x.LastName))
            .ToList();

        return ServiceResult<List<PersonelDto>>.SuccessOk(personelDtoList);
    }
}