namespace MyCleanArchTemplate.Application.Abstractions.Messaging;

public interface IMessageProducer
{
    Task ProduceAsync(string topic, string key, string value, CancellationToken token);
}
