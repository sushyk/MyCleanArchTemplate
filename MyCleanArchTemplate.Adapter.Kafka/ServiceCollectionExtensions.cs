using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyCleanArchTemplate.Application.Abstractions.Messaging;

namespace MyCleanArchTemplate.Adapter.Kafka;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddKafka(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ProducerConfig>(configuration.GetSection("Kafka:ProducerSettings"));

        services.AddSingleton<IMessageProducer, KafkaProducer>();

        return services;
    }
}
