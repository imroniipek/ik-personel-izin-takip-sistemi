using MediatR;
using Personel.Personel.Application.Abstraction;
using Shared.ServiceResult;

namespace Personel.Personel.Application.Features.Personel.GetAllPersonelsByDepartmentId;

public class GetAllPersonelByDepartmentIdHandler(IPersonelRepository personelRepository):IRequestHandler<GetAllPersonelsByDepartmentId,ServiceResult<List<GetPersonelByDepartmentIdDto>>>
{
    public async Task<ServiceResult<List<GetPersonelByDepartmentIdDto>>> Handle(GetAllPersonelsByDepartmentId request, CancellationToken cancellationToken)
    {
       var thePersonelList=await  personelRepository.GetAllPersonelsByDepartmentIdAsync(request.DepartmentId);

       var thePersonelDtoList=thePersonelList.Select(x => new GetPersonelByDepartmentIdDto(x.Id,x.FirstName,x.LastName,x.Email,x.HireDate.ToString(),x.Department.Name)).ToList();

       return ServiceResult<List<GetPersonelByDepartmentIdDto>>.SuccessOk(thePersonelDtoList);
    }
}