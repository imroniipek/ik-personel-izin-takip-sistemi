using MediatR;
using Personel.Personel.Application.Abstraction;
using Shared.ServiceResult;

namespace Personel.Personel.Application.Features.Personel.DeleteTheManager;

public class DeleteTheManagerCommandHandler(IDepartmentRepository repository)
    : IRequestHandler<DeleteTheManagerCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(DeleteTheManagerCommand request, CancellationToken cancellationToken)
    {
        var theResult = await repository.DeleteTheManagerByDepartmentIdAsync(request.DepartmentId);
        return theResult ? ServiceResult.SuccessAsNoContent() : ServiceResult.ErrorAsNoFound();
    }
}