using Mediator;
using MyCleanArchTemplate.Core.Shared;
using MyCleanArchTemplate.Domain.Customers;

namespace MyCleanArchTemplate.Application.Customers.CreateCustomer;

public sealed record CreateCustomerCommand(string Name, string Email) : ICommand<Result<Customer>>;
