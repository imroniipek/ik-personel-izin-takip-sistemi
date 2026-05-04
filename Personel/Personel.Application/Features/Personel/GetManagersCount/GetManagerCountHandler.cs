using MediatR;
using Personel.Personel.Application.Abstraction;
using Shared.ServiceResult;

namespace Personel.Personel.Application.Features.Personel.GetManagersCount;

public class GetManagerCountHandler(IDepartmentRepository repository):IRequestHandler<GetManagerCount,ServiceResult<int>>
{
    public async Task<ServiceResult<int>> Handle(GetManagerCount request, CancellationToken cancellationToken)
    {
       int managerCount= await repository.GetManagersCountAsync();

       return ServiceResult<int>.SuccessOk(managerCount);

    }
}