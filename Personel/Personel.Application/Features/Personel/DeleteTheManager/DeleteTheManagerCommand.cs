using MediatR;
using Shared.ServiceResult;

namespace Personel.Personel.Application.Features.Personel.DeleteTheManager;

public record DeleteTheManagerCommand(int DepartmentId):IRequest<ServiceResult>{}