using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyCleanArchTemplate.Application.Abstractions.Messaging;

namespace MyCleanArchTemplate.Adapter.Kafka;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddKafka(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptionsWithValidateOnStart<KafkaProducerSettings>()
            .BindConfiguration("Kafka:ProducerSettings")
            .ValidateDataAnnotations();

        services.AddOptionsWithValidateOnStart<KafkaConsumerSettings>()
            .BindConfiguration("Kafka:ConsumerSettings")
            .ValidateDataAnnotations();

        services.AddSingleton<IMessageProducer, KafkaMessageProducer>();
        services.AddHostedService<KafkaMessageConsumer>();

        return services;
    }
}
