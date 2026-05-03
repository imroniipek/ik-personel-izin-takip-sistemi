using System.Net;
using Approval.Approval.Application.Abstraction.Client;
using MediatR;
using Shared.ServiceResult;

namespace Approval.Approval.Application.Features.GetAllPersonelPendingListByManagerId;

public class GetAllPersonelPendingListByManagerIdHandler(
    IGetPersonelByManagerIdFromServices managerIdFromServices,
    IGetPendingListByPersonelId pendingListService):IRequestHandler<GetAllPersonelPendingListByManagerId,ServiceResult<List<GetAllPersonelPendingListByManagerIdDto>>>
{
    public async Task<ServiceResult<List<GetAllPersonelPendingListByManagerIdDto>>> Handle(GetAllPersonelPendingListByManagerId request, CancellationToken cancellationToken)
    {
        var personelList = await managerIdFromServices.GetPersonelByManagerId(request.ManagerId);

        if (!personelList.Any())
        {
            return ServiceResult<List<GetAllPersonelPendingListByManagerIdDto>>.Error(
                "Personeller bulunamadı",
                "Bu manager'a bağlı personel bulunamadı.",
                HttpStatusCode.NotFound
            );
        }
        var leaveList=await pendingListService.GetPendingLeaveListByListPersonelIdDto(personelList);

        List<GetAllPersonelPendingListByManagerIdDto> personelsListResponse = [];

        foreach (var personel in personelList)
        {
            foreach (var leave  in leaveList)
            {
                if (personel.PersonelId == leave.PersonelId)
                {
                    var personelPendingListByManagerIdDto =
                        new GetAllPersonelPendingListByManagerIdDto(personel.FirstName, personel.LastName,
                           leave.Id, leave.StartedDate, leave.EndedDate);

                    personelsListResponse.Add(personelPendingListByManagerIdDto);
                }
            }
          
        }
        return ServiceResult<List<GetAllPersonelPendingListByManagerIdDto>>.SuccessOk(personelsListResponse);
    }
}