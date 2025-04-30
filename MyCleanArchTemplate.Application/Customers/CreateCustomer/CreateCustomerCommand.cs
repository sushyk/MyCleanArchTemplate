using MyCleanArchTemplate.Application.Abstractions.Messaging;
using MyCleanArchTemplate.Domain.Customers;

namespace MyCleanArchTemplate.Application.Customers.CreateCustomer;

public sealed record CreateCustomerCommand(Customer Customer) : ICommand<Customer>;

