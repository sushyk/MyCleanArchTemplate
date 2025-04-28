using Microsoft.Extensions.DependencyInjection;

namespace MyCleanArchTemplate.Adapter.Persistence;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {

        return services;
    }
}
