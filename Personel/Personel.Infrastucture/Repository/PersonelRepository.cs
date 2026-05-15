using Microsoft.EntityFrameworkCore;
using Personel.Personel.Application.Abstraction;
using Personel.Personel.Application.Features.Personel.Dtos;
using Personel.Personel.Infrastucture.Context;

namespace Personel.Personel.Infrastucture.Repository;

public class PersonelRepository(PersonelDbContext context) : IPersonelRepository
{
    public async Task<Domain.Personel?> GetPersonelByIdAsync(int personelId)
    {
        return await context.Personels.FindAsync(personelId);
    }

    public async Task<Domain.Personel> CreateNewPersonelAsync(Domain.Personel personel)
    {
        var entityEntry = await context.Personels.AddAsync(personel);
        await context.SaveChangesAsync();

        return entityEntry.Entity;
    }

    public async Task<List<Domain.Personel>> GetAllPersonelsAsync()=>await context.Personels.AsNoTracking().Include(x => x.Department).ToListAsync();
    public async Task<int> GetAllPersonelsCountAsync()
    {
        return await context.Personels
            .AsNoTracking()
            .CountAsync();
    }

    public Task<List<Domain.Personel>> GetAllPersonelsByDepartmentIdAsync(int departmentId)=>context.Personels.AsNoTracking().Include(x => x.Department).Where(x => x.DepartmentId == departmentId).ToListAsync();

    public async Task<Domain.Personel?> GetPersonelByEmailAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return null;

        return await context.Personels
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());
    }

    public async Task<bool> DeletePersonelByIdAsync(int personelId)
    {
        var theEntity= context.Personels.FirstOrDefault(x => x.Id == personelId);

        if (theEntity != null)
        {
            context.Personels.Remove(theEntity);
            await context.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task<bool> UpdateThePersonelAsync(int personelId, Domain.Personel personel)
    {
        var theEntity = await context.Personels
            .FirstOrDefaultAsync(x => x.Id == personelId);

        if (theEntity == null)
            return false;

        theEntity.FirstName = personel.FirstName;
        theEntity.LastName = personel.LastName;
        theEntity.Email = personel.Email;
        theEntity.HireDate = personel.HireDate;
        theEntity.DepartmentId = personel.DepartmentId;

        await context.SaveChangesAsync();

        return true;
    }

    public async Task<Domain.Personel?> GetThePersonelByPersonelId(int personelId)
    {
        return await context.Personels.AsNoTracking().FirstOrDefaultAsync(x => x.Id == personelId);
    }

    public async Task<List<PersonelDto>> GetAllPersonelsWithoutManager(int managerId, int departmentId)
    {
        var personelList = await context.Personels
            .AsNoTracking()
            .Include(x => x.Department)
            .Where(x => x.DepartmentId == departmentId && x.Id != managerId).Select(x => new PersonelDto(x.Id, x.FirstName, x.LastName, x.Email, x.HireDate, x.Department.Name))
            .ToListAsync();
        return personelList;
    }
}