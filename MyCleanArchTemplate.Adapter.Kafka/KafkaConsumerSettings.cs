using System.ComponentModel.DataAnnotations;

namespace MyCleanArchTemplate.Adapter.Kafka;

internal class KafkaConsumerSettings
{
    [Required]
    public string BootstrapServers { get; set; }

    [Required]
    public string GroupId { get; set; }
}
