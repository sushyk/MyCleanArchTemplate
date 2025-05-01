using Mediator;
using MyCleanArchTemplate.Core.Shared;
using MyCleanArchTemplate.Domain.Customers;

namespace MyCleanArchTemplate.Application.Customers.GetCustomer;
public sealed record GetCustomerQuery(long CustomerId) : IQuery<Result<Customer>>;
