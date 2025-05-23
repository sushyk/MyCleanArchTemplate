using FluentValidation;
using Microsoft.Extensions.Logging;

namespace MyCleanArchTemplate.Application.Customers.CreateCustomer;

internal sealed class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator(ILogger<CreateCustomerCommandValidator> logger)
    {
        logger.LogDebug("Beginning validation of CreateCustomerCommand");

        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);

        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(50);

        logger.LogDebug("Finished validation of CreateCustomerCommand");
    }
}
