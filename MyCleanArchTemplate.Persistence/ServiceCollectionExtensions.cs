using Microsoft.Extensions.DependencyInjection;

namespace MyCleanArchTemplate.Persistence;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {

        return services;
    }
}
