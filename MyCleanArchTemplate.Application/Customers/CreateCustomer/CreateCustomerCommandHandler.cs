using MyCleanArchTemplate.Application.Abstractions.Messaging;
using MyCleanArchTemplate.Application.Abstractions.Persistence;
using MyCleanArchTemplate.Domain.Customers;

namespace MyCleanArchTemplate.Application.Customers.CreateCustomer;

public sealed class CreateCustomerCommandHandler(
    ICustomerRepository customerRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateCustomerCommand, Customer>
{
    public async Task<Customer> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
    {
        customerRepository.CreateCustomer(command.Customer);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return command.Customer;
    }
}
