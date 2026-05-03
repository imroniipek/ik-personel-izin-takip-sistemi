using Refit;
using Shared.Dtos;

namespace Approval.Approval.Application.Abstraction.Client;

public interface IGetPersonelByManagerIdFromServices
{
    [Get("/api/getPersonelByManagerId")]
    Task<List<PersonelDto>> GetPersonelByManagerId(int managerId);
}