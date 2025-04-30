using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCleanArchTemplate.Domain.Customers;

public interface ICustomerRepository
{
    Task<Customer> GetById(long customerId, CancellationToken cancellationToken);

    void CreateCustomer(Customer customer);
}
