using Mediator;
using MyCleanArchTemplate.Core.Shared;

namespace MyCleanArchTemplate.Application.Customers.CreateCustomer;

public sealed record CreateCustomerCommand(string Name, string Email) : ICommand<Result<CustomerDto>>;
