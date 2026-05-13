using MediatR;
using Personel.Personel.Application.Abstraction;
using Shared.ServiceResult;

namespace Personel.Personel.Application.Features.Personel.GetPersonelByPersonelId;

public class GetPersonelByPersonelIdQueryHandler(IPersonelRepository repository):IRequestHandler<GetPersonelByPersonelIdQuery,ServiceResult<Domain.Personel>>
{
    public async Task<ServiceResult<Domain.Personel>> Handle(GetPersonelByPersonelIdQuery request, CancellationToken cancellationToken)
    {

        var thePersonel=await repository.GetPersonelByIdAsync(request.PersonelId);

        if (thePersonel != null)
        {
            return ServiceResult<Domain.Personel>.SuccessOk(thePersonel);
        }

        return ServiceResult<Domain.Personel>.ErrorAsNotFound();
    }
}