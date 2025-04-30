using MyCleanArchTemplate.Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCleanArchTemplate.Domain.Customers;

public static class CustomerErrors
{
    public static Error NotFound(long customerId) => Error.NotFound(
        "Customers.NotFound",
        $"The customer with the Id = {customerId} was not found");
}
