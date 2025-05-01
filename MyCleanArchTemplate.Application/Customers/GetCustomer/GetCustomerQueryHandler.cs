using Mediator;
using MyCleanArchTemplate.Core.Shared;
using MyCleanArchTemplate.Domain.Customers;

namespace MyCleanArchTemplate.Application.Customers.GetCustomer;

public class GetCustomerQueryHandler(
    ICustomerRepository customerRepository
    ) : IQueryHandler<GetCustomerQuery, Result<CustomerDto>>
{
    public async ValueTask<Result<CustomerDto>> Handle(GetCustomerQuery query, CancellationToken cancellationToken)
    {
        Customer customer = await customerRepository.GetById(query.CustomerId, cancellationToken);

        if (customer is null)
        {
            return Result.Failure<CustomerDto>(CustomerErrors.NotFound(query.CustomerId));
        }
        
        return Result.Success(new CustomerDto(customer.CustomerId, customer.Name, customer.Email));
    }
}
