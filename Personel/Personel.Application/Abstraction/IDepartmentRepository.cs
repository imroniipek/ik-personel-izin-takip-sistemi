using System.Collections.Generic;
using System.Threading.Tasks;
using Personel.Personel.Application.Features.Department.GetAllDepartmentWithNames;
using Personel.Personel.Domain;

namespace Personel.Personel.Application.Abstraction;

public interface IDepartmentRepository
{
    Task<Department> CreateNewDepartmentAsync(Department department);

    Task<List<Domain.Personel>> GetPersonelsByManagerIdAsync(int managerId);

    Task<bool> IsExistAsync(int departmentId);

    Task<Department?> GetDepartmentByDepartmentIdAsync(int departmentId);

    Task Update(Department department);

    Task<int> GetDepartmentCountAsync();

    Task<List<DepartmentDto>> GetAllDepartmentsWithNamesAsync();

    Task<string?> GetManagerNameByIdAsync(int departmentId);

    Task<bool> IsThisAManager(int personelId);

    Task<List<Domain.Personel>> GetPersonelsByManagerIdWithoutManagerAsync(int managerId);

    Task<int> GetManagersCountAsync();

    Task <bool> DeleteTheManagerByDepartmentIdAsync(int departmentId);

    Task UpdateTheManager(int managerId, int departmentId);
}