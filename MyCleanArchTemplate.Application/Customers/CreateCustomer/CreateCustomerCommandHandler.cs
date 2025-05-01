using FluentValidation;
using Mediator;
using Microsoft.Extensions.Logging;
using MyCleanArchTemplate.Application.Abstractions.Persistence;
using MyCleanArchTemplate.Core.Shared;
using MyCleanArchTemplate.Domain.Customers;

namespace MyCleanArchTemplate.Application.Customers.CreateCustomer;

public sealed class CreateCustomerCommandHandler(
    ICustomerRepository customerRepository,
    IUnitOfWork unitOfWork,
    ILogger<CreateCustomerCommandHandler> logger) : ICommandHandler<CreateCustomerCommand, Result<Customer>>
{
    public async ValueTask<Result<Customer>> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
    {
        Customer newCustomer = new()
        {
            Name = command.Name,
            Email = command.Email,
            CreatedDate = DateTime.UtcNow
        };
        customerRepository.CreateCustomer(newCustomer);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Created customer with Id {CustomerId} and Email {CustomerEmail}", newCustomer.CustomerId, newCustomer.Email);

        return Result.Success(newCustomer);
    }
}
