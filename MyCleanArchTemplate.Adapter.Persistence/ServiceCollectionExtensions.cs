using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyCleanArchTemplate.Adapter.Persistence.Repositories;
using MyCleanArchTemplate.Application.Abstractions.Persistence;
using MyCleanArchTemplate.Domain.Customers;

namespace MyCleanArchTemplate.Adapter.Persistence;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfigurationManager configurationManager)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(configurationManager.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<ICustomerRepository, CustomerRepository>();

        return services;
    }
}
