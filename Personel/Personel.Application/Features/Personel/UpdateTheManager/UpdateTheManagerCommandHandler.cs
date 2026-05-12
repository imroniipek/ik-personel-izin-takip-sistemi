using MediatR;
using Personel.Personel.Application.Abstraction;
using Shared.ServiceResult;

namespace Personel.Personel.Application.Features.Personel.UpdateTheManager;

public class UpdateTheManagerCommandHandler(IDepartmentRepository repository):IRequestHandler<UpdateTheManagerCommand,ServiceResult>
{
    public async Task<ServiceResult> Handle(UpdateTheManagerCommand request, CancellationToken cancellationToken)
    {
        await repository.UpdateTheManager(request.ManagerId, request.DepartmentId);

        return ServiceResult.SuccessAsNoContent();
    }
}