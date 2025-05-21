using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyCleanArchTemplate.Adapter.Persistence.Repositories;
using MyCleanArchTemplate.Application.Abstractions.Persistence;
using MyCleanArchTemplate.Domain.Customers;

namespace MyCleanArchTemplate.Adapter.Persistence;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<ICustomerRepository, CustomerRepository>();

        return services;
    }
}
