using System.Net;
using Leaves.Leaves.Application.Abstraction.Repositories;
using MediatR;
using Personel.Personel.Application.Abstraction;
using Personel.Personel.Application.Features.Personel.GetPersonelByEmail;
using Shared.ServiceResult;

namespace Personel.Personel.Application.Features.Personel.GetManagerByEmail;

public class GetManagerByEmailQueryHandler(IPersonelRepository personelRepository,IDepartmentRepository departmentRepository):IRequestHandler<GetManagerByEmailQuery,ServiceResult<GetPersonelByEmailResponse>>
{
    public async Task<ServiceResult<GetPersonelByEmailResponse>> Handle(GetManagerByEmailQuery request, CancellationToken cancellationToken)
    {
        var personel = await personelRepository.GetPersonelByEmailAsync(request.Email);

        if (personel is null)
        {
            return ServiceResult<GetPersonelByEmailResponse>.Error(
                "Personel bulunamadı",
                "Bu email ile kayıtlı personel bulunamadı.",
                HttpStatusCode.NotFound
            );
        }

        var theResult=await departmentRepository.IsThisAManager(personel.Id);
        
        if (theResult)
        {
            var response = new GetPersonelByEmailResponse(
                personel.Id,
                personel.FirstName,
                personel.LastName,
                personel.Email
            );

            return ServiceResult<GetPersonelByEmailResponse>.SuccessOk(response);
        }
        return ServiceResult<GetPersonelByEmailResponse>.Error(
            "Bu Emaili Ait bir Manager Bulunamadı",
            "Bu email ile kayıtlı manager bulunamadı.",
            HttpStatusCode.NotFound
        );
    }
}