using Mediator;
using MyCleanArchTemplate.Application.Abstractions.Messaging;
using MyCleanArchTemplate.Core.Shared;
using MyCleanArchTemplate.Domain.Customers;

namespace MyCleanArchTemplate.Application.Customers.GetCustomer;
public sealed record GetCustomerQuery(long CustomerId) : IRequest<Result<Customer>>;
