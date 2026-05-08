using Microsoft.EntityFrameworkCore;
using Personel.Personel.Infrastucture.Context;

namespace Personel.Personel.Infrastucture.Extension;

public static class  InfrastructureServiceExtension
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<PersonelDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("SqlServer"));
        });

        return services;
    }
}