namespace MyCleanArchTemplate.Domain.Entities;

public class Customer
{
    public long CustomerId { get; set; }

    public required string Name { get; set; }

    public string Email { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; } = DateTime.Now;

}
