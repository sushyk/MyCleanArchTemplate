using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using MyCleanArchTemplate.Domain.Customers;
using System.Text.Json;

namespace MyCleanArchTemplate.Adapter.Persistence.Repositories;

public sealed class CustomerRepository(
    AppDbContext dbContext, IDistributedCache distributedCache) : ICustomerRepository
{
    public void CreateCustomer(Customer customer)
    {
        dbContext.Customers.Add(customer);
    }

    public async Task<Customer> GetById(long customerId, CancellationToken cancellationToken)
    {
        var cacheKey = $"customer-{customerId}";

        string? cachedCustomer = await distributedCache.GetStringAsync(cacheKey);

        Customer customer;
        if (string.IsNullOrEmpty(cachedCustomer))
        {
            customer = await dbContext.Customers.SingleOrDefaultAsync(x => x.CustomerId == customerId, cancellationToken);

            if (customer == null)
            {
                return customer;
            }

            await distributedCache.SetStringAsync(cacheKey, JsonSerializer.Serialize(customer), cancellationToken);

            return customer;
        }

        customer = JsonSerializer.Deserialize<Customer>(cachedCustomer);

        return customer;
    }
}
