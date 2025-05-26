namespace MyCleanArchTemplate.Adapter.Kafka;

internal class KafkaConsumerSettings
{
    public string BootstrapServers { get; set; }
    public string GroupId { get; set; }
}
