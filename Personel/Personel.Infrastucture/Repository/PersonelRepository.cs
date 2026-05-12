using Microsoft.EntityFrameworkCore;
using Personel.Personel.Application.Abstraction;
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
    
}