using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Personel.Personel.Application.Abstraction;
using Shared.ServiceResult;

namespace Personel.Personel.Application.Features.Personel.DeleteThePersonel;

public class DeleteThePersonelCommandHandler(IPersonelRepository repository):IRequestHandler<DeleteThePersonelCommand,ServiceResult<bool>>
{
    public async Task<ServiceResult<bool>> Handle(DeleteThePersonelCommand request, CancellationToken cancellationToken)
    {
         var theResponse=await repository.DeletePersonelByIdAsync(request.PersonelId);

         if (theResponse)
         {
             return ServiceResult<bool>.SuccessAsNoContent();
         }

         return ServiceResult<bool>.ErrorAsNotFound();
    }
}