using Microsoft.EntityFrameworkCore;
using Personel.Personel.Application.Abstraction;
using Personel.Personel.Application.Features.Department.GetAllDepartmentWithNames;
using Personel.Personel.Domain;
using Personel.Personel.Infrastucture.Context;
namespace Personel.Personel.Infrastucture.Repository;

public class DepartmentRepository(PersonelDbContext context) : IDepartmentRepository
{
    public async Task<Department> CreateNewDepartmentAsync(Department department)
    {
        var entityEntry = await context.Departments.AddAsync(department);
        await context.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task<List<Domain.Personel>> GetPersonelsByManagerIdAsync(int managerId)
    {
        var department = await context.Departments
            .Include(x => x.Personels)
            .FirstOrDefaultAsync(x => x.ManagerId == managerId);

        return department?.Personels.ToList() ?? new List<Domain.Personel>();
    }

    public async Task<bool> IsExistAsync(int departmentId)
    {
        return await context.Departments.AnyAsync(x => x.Id == departmentId);
    }

    public async Task<Department?> GetDepartmentByDepartmentIdAsync(int departmentId)
    {
        return await context.Departments
            .FirstOrDefaultAsync(x => x.Id == departmentId);
    }

    public async Task Update(Department department)
    {
        context.Departments.Update(department);
        await context.SaveChangesAsync();
    }

    public async Task<int> GetDepartmentCountAsync()
    {
        return await context.Departments.AsNoTracking().CountAsync();
    }

    public async Task<List<DepartmentDto>> GetAllDepartmentsWithNamesAsync()
    {
        return  await context.Departments.AsNoTracking()
            .Select(x => new DepartmentDto(x.Id, x.Name, x.Manager != null ? $"{x.Manager.FirstName} {x.Manager.LastName}" : "-")).ToListAsync();
    }

    public async Task<string?> GetManagerNameByIdAsync(int departmentId)
    {
        return await context.Departments
            .AsNoTracking()
            .Where(d => d.Id == departmentId)
            .Select(d => d.Manager != null ? d.Manager.FirstName + " " + d.Manager.LastName : null)
            .FirstOrDefaultAsync();
    }
    public async Task<bool> IsThisAManager(int personelId)
    {
        return await context.Departments
            .AsNoTracking()
            .AnyAsync(x => x.ManagerId == personelId);
    }
    public async Task<List<Domain.Personel>> GetPersonelsByManagerIdWithoutManagerAsync(int managerId)
    {
        var department = await context.Departments
            .Include(x => x.Personels)
            .FirstOrDefaultAsync(x => x.ManagerId == managerId);

        return department?.Personels
            .Where(x => x.Id != managerId)
            .ToList() ?? new List<Domain.Personel>();
    }

    public async Task<int> GetManagersCountAsync()
    {
        var managerCount = await context.Departments.AsNoTracking().Where(x => x.ManagerId != null)
            .Select(x => x.ManagerId).Distinct().CountAsync();
        return managerCount;
    }
}