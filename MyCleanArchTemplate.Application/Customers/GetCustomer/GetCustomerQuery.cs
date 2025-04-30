using MyCleanArchTemplate.Application.Abstractions.Messaging;
using MyCleanArchTemplate.Domain.Customers;

namespace MyCleanArchTemplate.Application.Customers.GetCustomer;

public sealed record GetCustomerQuery(long customerId) : IQuery<Customer>;
