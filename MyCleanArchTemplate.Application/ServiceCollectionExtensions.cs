using FluentValidation;
using Mediator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyCleanArchTemplate.Application.Behaviours;

namespace MyCleanArchTemplate.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediator(options =>
        {
            options.ServiceLifetime = ServiceLifetime.Scoped;
        });

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
          
        services.AddValidatorsFromAssembly(typeof(ServiceCollectionExtensions).Assembly, includeInternalTypes: true);

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis");
            options.InstanceName = "SampleInstance";
        });

        return services;
    }
}
