using Mediator;
using MyCleanArchTemplate.Application.Abstractions.Messaging;
using MyCleanArchTemplate.Application.Abstractions.Persistence;
using MyCleanArchTemplate.Domain.Customers;

namespace MyCleanArchTemplate.Application.Customers.CreateCustomer;

public sealed class CreateCustomerCommandHandler(
    ICustomerRepository customerRepository,
    IUnitOfWork unitOfWork) : Mediator.IRequestHandler<CreateCustomerCommand, Customer>
{
    public async ValueTask<Customer> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
    {
        Customer newCustomer = new()
        {
            Name = command.Name,
            Email = command.Email,
        };
        customerRepository.CreateCustomer(newCustomer);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return newCustomer;
    }
}
