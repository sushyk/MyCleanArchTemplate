using MyCleanArchTemplate.Application.Abstractions.Messaging;
using MyCleanArchTemplate.Core.Shared;
using MyCleanArchTemplate.Domain.Customers;

namespace MyCleanArchTemplate.Application.Customers.GetCustomer;

public class GetCustomerQueryHandler(
    ICustomerRepository customerRepository
    ) : IQueryHandler<GetCustomerQuery, Customer>
{
    public async Task<Result<Customer>> Handle(GetCustomerQuery query, CancellationToken cancellationToken)
    {
        Customer customer = await customerRepository.GetById(query.CustomerId, cancellationToken);

        if (customer == null)
        {
            return Result.Failure<Customer>(CustomerErrors.NotFound(query.CustomerId));
        }

        return Result.Success(customer);
    }
}
