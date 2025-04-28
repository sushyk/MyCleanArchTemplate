using Microsoft.Extensions.DependencyInjection;

namespace MyCleanArchTemplate.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddHealthChecks();

        return services;
    }

}
