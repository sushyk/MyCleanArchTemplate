using Confluent.Kafka;
using Confluent.Kafka.Extensions.Diagnostics;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MyCleanArchTemplate.Adapter.Kafka;

internal class KafkaMessageConsumer : BackgroundService
{
    private const string Topic = "customers";

    private readonly IConsumer<string, string> _consumer;
    private readonly ILogger<KafkaMessageConsumer> _logger;

    public KafkaMessageConsumer(ILogger<KafkaMessageConsumer> logger, IOptions<KafkaConsumerSettings> settingsOptions)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        KafkaConsumerSettings settings = settingsOptions.Value;
        ConsumerConfig config = new()
        {
            BootstrapServers = settings.BootstrapServers,
            GroupId = settings.GroupId,
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
        _consumer = new ConsumerBuilder<string, string>(config).Build();
    }

    protected override Task ExecuteAsync(CancellationToken cancellationToken)
    {
        return Task.Run(() => StartConsumerLoop(cancellationToken), cancellationToken);
    }

    private void StartConsumerLoop(CancellationToken cancellationToken)
    {
        _consumer.Subscribe(Topic);

        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                _consumer.ConsumeWithInstrumentation((consumeResult, token) =>
                {
                    _logger.LogInformation("Read message with Key: {Key} and value {Value} at offset {Offset}", consumeResult.Message.Key, consumeResult.Message.Value, consumeResult.Offset);
                    return Task.CompletedTask;
                }, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Kafka consumer has requested to terminate.");
                break;
            }
            catch (ConsumeException e)
            {
                // Consumer errors should generally be ignored (or logged) unless fatal.
                _logger.LogError("Consume error: {Reason}", e.Error.Reason);

                if (e.Error.IsFatal)
                {
                    // https://github.com/edenhill/librdkafka/blob/master/INTRODUCTION.md#fatal-consumer-errors
                    _logger.LogCritical("Fatal error in kafka Consumer.");
                    break;
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Exception occured: {ex}", e.ToString());
                break;
            }
        }
    }

    public override void Dispose()
    {
        _consumer.Close(); // Commit offsets and leave the group cleanly.
        _consumer.Dispose();

        base.Dispose();
    }
}
