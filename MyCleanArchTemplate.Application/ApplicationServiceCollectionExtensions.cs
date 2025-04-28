using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace MyCleanArchTemplate.Application;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(ApplicationServiceCollectionExtensions).Assembly;

        services.AddMediator(options => 
            options.ServiceLifetime = ServiceLifetime.Scoped);

        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}
