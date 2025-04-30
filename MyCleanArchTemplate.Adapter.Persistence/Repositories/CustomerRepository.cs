using Microsoft.EntityFrameworkCore;
using MyCleanArchTemplate.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCleanArchTemplate.Adapter.Persistence.Repositories;

public sealed class CustomerRepository(
    AppDbContext dbContext) : ICustomerRepository
{
    public void CreateCustomer(Customer customer)
    {
        dbContext.Customers.Add(customer);
    }

    public async Task<Customer> GetById(long customerId, CancellationToken cancellationToken)
    {
        return await dbContext.Customers.SingleAsync(x => x.CustomerId == customerId, cancellationToken);
    }
}
