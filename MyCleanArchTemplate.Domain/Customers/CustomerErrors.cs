using MyCleanArchTemplate.Core.Shared;

namespace MyCleanArchTemplate.Domain.Customers;

public static class CustomerErrors
{
    public static Error NotFound(long customerId) => Error.NotFound(
        "Customers.NotFound",
        $"The customer with the Id = {customerId} was not found");
}
