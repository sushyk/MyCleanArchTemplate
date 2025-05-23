using Mediator;
using Microsoft.Extensions.Logging;
using MyCleanArchTemplate.Core.Shared;
using MyCleanArchTemplate.Domain.Customers;

namespace MyCleanArchTemplate.Application.Customers.GetCustomer;

public class GetCustomerQueryHandler(
    ICustomerRepository customerRepository,
    ILogger<GetCustomerQueryHandler> logger
    ) : IQueryHandler<GetCustomerQuery, Result<CustomerDto>>
{
    public async ValueTask<Result<CustomerDto>> Handle(GetCustomerQuery query, CancellationToken cancellationToken)
    {
        Customer customer = await customerRepository.GetById(query.CustomerId, cancellationToken);

        if (customer is null)
        {
            logger.LogError("Customer with Id {CustomerId} not found.", query.CustomerId);
            return Result.Failure<CustomerDto>(CustomerErrors.NotFound(query.CustomerId));
        }

        logger.LogInformation("Customer with Id {CustomerId} found.", query.CustomerId);

        return Result.Success(new CustomerDto(customer.CustomerId, customer.Name, customer.Email));
    }
}
