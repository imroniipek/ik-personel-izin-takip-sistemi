using System.Collections.Generic;
using System.Threading.Tasks;

namespace Personel.Personel.Application.Abstraction;

public interface IPersonelRepository
{

    Task<Domain.Personel?> GetPersonelByIdAsync(int personelId);

    Task<Domain.Personel> CreateNewPersonelAsync(Domain.Personel personel);

    Task<List<Domain.Personel>> GetAllPersonelsAsync();

    Task<int> GetAllPersonelsCountAsync();

    Task<List<Domain.Personel>> GetAllPersonelsByDepartmentIdAsync(int departmentId);

    Task<Domain.Personel?> GetPersonelByEmailAsync(string email);
    
}