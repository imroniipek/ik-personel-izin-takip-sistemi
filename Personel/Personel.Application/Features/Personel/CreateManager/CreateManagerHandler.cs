using System.Net;
using MediatR;
using Personel.Personel.Application.Abstraction;
using Shared.ServiceResult;

namespace Personel.Personel.Application.Features.Personel.CreateManager;

public class CreateManagerHandler(IPersonelRepository repository, IDepartmentRepository departmentRepository) : IRequestHandler<CreateManagerCommand, ServiceResult<CreateManagerResponse>>
{
    public async Task<ServiceResult<CreateManagerResponse>> Handle(
        CreateManagerCommand request,
        CancellationToken cancellationToken)
    {
        var theDepartment = await departmentRepository.GetDepartmentByDepartmentIdAsync(request.DepartmentId);

        if (theDepartment is null)
        {
            return ServiceResult<CreateManagerResponse>.Error(
                "Department not found",
                "Böyle bir department bulunamadı.",
                HttpStatusCode.NotFound);
        }

        if (theDepartment.ManagerId is not null && theDepartment.ManagerId != 0)
        {
            return ServiceResult<CreateManagerResponse>.Error(
                "Manager already exists",
                "Bu department için zaten manager atanmış.",
                HttpStatusCode.Conflict);
        }

        var theManager = await repository.GetPersonelByIdAsync(request.ManagerId);

        if (theManager is null)
        {
            return ServiceResult<CreateManagerResponse>.Error(
                "Personel not found",
                "Manager olarak atanacak personel bulunamadı.",
                HttpStatusCode.NotFound);
        }

        if (theManager.DepartmentId != request.DepartmentId)
        {
            return ServiceResult<CreateManagerResponse>.Error(
                "Invalid manager",
                "Seçilen personel bu departmana ait değil.",
                HttpStatusCode.BadRequest);
        }

        theDepartment.ManagerId = request.ManagerId;

        await departmentRepository.Update(theDepartment);

        var response = new CreateManagerResponse(
            theDepartment.Id,
            theDepartment.Name,
            theManager.Id,
            $"{theManager.FirstName} {theManager.LastName}"
        );

        return ServiceResult<CreateManagerResponse>.SuccessCreatedOk(
            response,
            $"/api/departments/{theDepartment.Id}/manager"
        );
    }
}