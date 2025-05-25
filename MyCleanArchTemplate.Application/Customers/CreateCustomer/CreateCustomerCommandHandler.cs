using Mediator;
using Microsoft.Extensions.Logging;
using MyCleanArchTemplate.Application.Abstractions.Messaging;
using MyCleanArchTemplate.Application.Abstractions.Persistence;
using MyCleanArchTemplate.Core.Shared;
using MyCleanArchTemplate.Domain.Customers;
using System.Text.Json;

namespace MyCleanArchTemplate.Application.Customers.CreateCustomer;

public sealed class CreateCustomerCommandHandler(
    ICustomerRepository customerRepository,
    IUnitOfWork unitOfWork,
    ILogger<CreateCustomerCommandHandler> logger,
    IMessageProducer messageProducer) : ICommandHandler<CreateCustomerCommand, Result<CustomerDto>>
{
    public async ValueTask<Result<CustomerDto>> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
    {
        Customer newCustomer = new()
        {
            Name = command.Name,
            Email = command.Email,
            CreatedDate = DateTime.UtcNow
        };
        customerRepository.CreateCustomer(newCustomer);
        logger.LogDebug("Customer with email {CustomerEmail} saved in repository.", command.Email);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Created customer with Id {CustomerId} and Email {CustomerEmail}", newCustomer.CustomerId, newCustomer.Email);

        await messageProducer.ProduceAsync("customers", command.Name.Length.ToString(), JsonSerializer.Serialize(newCustomer), cancellationToken);

        return Result.Success(new CustomerDto(newCustomer.CustomerId, newCustomer.Name, newCustomer.Email));
    }
}
