using Mediator;
using MyCleanArchTemplate.Core.Shared;

namespace MyCleanArchTemplate.Application.Customers.GetCustomer;

public sealed record GetCustomerQuery(long CustomerId) : IQuery<Result<CustomerDto>>;
