using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MyCleanArchTemplate.Adapter.Persistence;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfigurationManager configurationManager)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(configurationManager.GetConnectionString("DefaultConnection"));
        });

        return services;
    }
}
