using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyCleanArchTemplate.Application.Abstractions.Messaging;

namespace MyCleanArchTemplate.Adapter.Kafka;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddKafka(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<KafkaProducerSettings>(configuration.GetSection("Kafka:ProducerSettings"));
        services.Configure<KafkaConsumerSettings>(configuration.GetSection("Kafka:ConsumerSettings"));

        services.AddSingleton<IMessageProducer, KafkaMessageProducer>();
        services.AddHostedService<KafkaMessageConsumer>();

        return services;
    }
}
