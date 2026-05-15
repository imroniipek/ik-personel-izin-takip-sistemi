using MediatR;
using Personel.Personel.Application.Abstraction;
using Personel.Personel.Application.Features.Personel.Dtos;
using Shared.ServiceResult;

namespace Personel.Personel.Application.Features.Personel.GetAllPersonelsWithoutManager;

public class GetAllPersonelsWithoutManagerQueryHandler(
    IPersonelRepository repository
) : IRequestHandler<GetAllPersonelsWithoutManagerQuery, ServiceResult<List<PersonelDto>>>
{
    public async Task<ServiceResult<List<PersonelDto>>> Handle(
        GetAllPersonelsWithoutManagerQuery request,
        CancellationToken cancellationToken)
    {
        var thePersonelList = await repository.GetAllPersonelsWithoutManager(
            request.ManagerId,
            request.DepartmentId
        );

        return ServiceResult<List<PersonelDto>>.SuccessOk(thePersonelList);
    }
}