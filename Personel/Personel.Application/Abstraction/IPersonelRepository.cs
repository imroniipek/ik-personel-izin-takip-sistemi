using Personel.Personel.Application.Features.Personel.Dtos;

namespace Personel.Personel.Application.Abstraction;

public interface IPersonelRepository
{

    Task<Domain.Personel?> GetPersonelByIdAsync(int personelId);

    Task<Domain.Personel> CreateNewPersonelAsync(Domain.Personel personel);

    Task<List<Domain.Personel>> GetAllPersonelsAsync();

    Task<int> GetAllPersonelsCountAsync();

    Task<List<Domain.Personel>> GetAllPersonelsByDepartmentIdAsync(int departmentId);

    Task<Domain.Personel?> GetPersonelByEmailAsync(string email);

    Task<bool> DeletePersonelByIdAsync(int personelId);

    Task<bool> UpdateThePersonelAsync(int personelId, Domain.Personel personel);

    Task<Domain.Personel? >GetThePersonelByPersonelId(int personelId);

    Task<List<PersonelDto>> GetAllPersonelsWithoutManager(int managerId, int departmentId);
}