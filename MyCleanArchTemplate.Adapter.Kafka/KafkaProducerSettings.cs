using System.ComponentModel.DataAnnotations;

namespace MyCleanArchTemplate.Adapter.Kafka;

internal class KafkaProducerSettings
{
    [Required]
    public string BootstrapServers { get; set; }
}
