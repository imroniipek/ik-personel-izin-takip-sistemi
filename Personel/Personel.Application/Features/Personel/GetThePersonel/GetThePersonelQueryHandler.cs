using MediatR;
using Personel.Personel.Application.Abstraction;
using Shared.ServiceResult;
namespace Personel.Personel.Application.Features.Personel.GetThePersonel;
public class GetThePersonelQueryHandler(IPersonelRepository personelRepository, IDepartmentRepository departmentRepository)
    : IRequestHandler<GetThePersonelQuery, ServiceResult<GetThePersonelQueryResponse>>
{
    public async Task<ServiceResult<GetThePersonelQueryResponse>> Handle(GetThePersonelQuery request,
        CancellationToken cancellationToken)
    {
        var thePersonel = await personelRepository.GetPersonelByIdAsync(request.PersonelId);

        if (thePersonel == null)
        {
            return ServiceResult<GetThePersonelQueryResponse>.ErrorAsNotFound();
        }

        var theDepartment = await departmentRepository.GetDepartmentByDepartmentIdAsync(thePersonel.DepartmentId);

        if (theDepartment == null)
        {
            return ServiceResult<GetThePersonelQueryResponse>.ErrorAsNotFound();
        }

        var theManagerName = await departmentRepository.GetManagerNameByIdAsync(thePersonel.DepartmentId);


        var theGetPersonelQueryResponse = new GetThePersonelQueryResponse(thePersonel.FirstName, thePersonel.LastName,
            theDepartment.Name, theManagerName ?? "-", thePersonel.HireDate.ToString());

        return ServiceResult<GetThePersonelQueryResponse>.SuccessOk(theGetPersonelQueryResponse);
    }
}