using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using MyCleanArchTemplate.Domain.Customers;
using System.Text.Json;

namespace MyCleanArchTemplate.Adapter.Persistence.Repositories;

public sealed class CustomerRepository(
    AppDbContext dbContext,
    IDistributedCache distributedCache,
    ILogger<CustomerRepository> logger) : ICustomerRepository
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
            logger.LogDebug("Could not find customer with Id {CustomerId} in cache with key {CacheKey}", customerId,  cacheKey);
            customer = await dbContext.Customers.SingleOrDefaultAsync(x => x.CustomerId == customerId, cancellationToken);

            if (customer == null)
            {
                logger.LogDebug("Could not find customer with Id {CustomerId} in the database", customerId);
                return customer;
            }

            await distributedCache.SetStringAsync(cacheKey, JsonSerializer.Serialize(customer), cancellationToken);
            logger.LogDebug("Set value for customer with Id {CustomerId} against {CacheKey} in cache", customerId, cacheKey);

            return customer;
        }

        customer = JsonSerializer.Deserialize<Customer>(cachedCustomer);
        logger.LogDebug("Successfully deserialized customer with Id {CustomerId}", customerId);

        return customer;
    }
}
