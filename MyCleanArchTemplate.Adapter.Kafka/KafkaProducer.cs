using Confluent.Kafka;
using Confluent.Kafka.Extensions.Diagnostics;
using Microsoft.Extensions.Options;
using MyCleanArchTemplate.Application.Abstractions.Messaging;

namespace MyCleanArchTemplate.Adapter.Kafka;

internal class KafkaProducer : IMessageProducer, IDisposable
{
    private readonly IProducer<string, string> kafkaHandler;

    public KafkaProducer(IOptions<ProducerConfig> producerConfig)
    {
        kafkaHandler = new ProducerBuilder<string, string>(producerConfig.Value).BuildWithInstrumentation();
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
