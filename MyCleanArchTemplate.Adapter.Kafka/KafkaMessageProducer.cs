using Confluent.Kafka;
using Confluent.Kafka.Extensions.Diagnostics;
using Microsoft.Extensions.Options;
using MyCleanArchTemplate.Application.Abstractions.Messaging;

namespace MyCleanArchTemplate.Adapter.Kafka;

internal class KafkaMessageProducer : IMessageProducer, IDisposable
{
    private readonly IProducer<string, string> kafkaHandler;

    public KafkaMessageProducer(IOptions<KafkaProducerSettings> kafkaProducerSettingsOptions)
    {
        KafkaProducerSettings settings = kafkaProducerSettingsOptions.Value;
        ProducerConfig config = new()
        {
            BootstrapServers = settings.BoostrapServers
        };
        kafkaHandler = new ProducerBuilder<string, string>(config).BuildWithInstrumentation();
    }

    public Task ProduceAsync(string topic, string key, string value, CancellationToken cancellationToken)
            => kafkaHandler.ProduceAsync(topic, new Message<string, string> { Key = key, Value = value }, cancellationToken);

    public void Dispose()
    {
        // Block until all outstanding produce requests have completed (with or without error).
        kafkaHandler.Flush();
        kafkaHandler.Dispose();
    }
}
