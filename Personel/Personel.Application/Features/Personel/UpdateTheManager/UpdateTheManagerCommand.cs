using MediatR;
using Shared.ServiceResult;

namespace Personel.Personel.Application.Features.Personel.UpdateTheManager;

public record UpdateTheManagerCommand(int ManagerId,int DepartmentId):IRequest<ServiceResult>{}