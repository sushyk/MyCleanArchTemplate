using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace MyCleanArchTemplate.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(ServiceCollectionExtensions).Assembly;

        services.AddMediator(options =>
        {
            options.ServiceLifetime = ServiceLifetime.Scoped;
        });
          
        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}
