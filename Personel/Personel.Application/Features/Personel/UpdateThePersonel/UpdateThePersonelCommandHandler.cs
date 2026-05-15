using MediatR;
using Personel.Personel.Application.Abstraction;
using Shared.ServiceResult;
namespace Personel.Personel.Application.Features.Personel.UpdateThePersonel;

public class UpdateThePersonelCommandHandler(IPersonelRepository repository):IRequestHandler<UpdateThePersonelCommand,ServiceResult>
{
    public async Task<ServiceResult> Handle(UpdateThePersonelCommand request, CancellationToken cancellationToken)
    {
        var theResult=await repository.UpdateThePersonelAsync(request.PersonelId, request.Personel);

        if (theResult){ return ServiceResult.SuccessAsNoContent();}

        return ServiceResult.ErrorAsNoFound();
    }
}