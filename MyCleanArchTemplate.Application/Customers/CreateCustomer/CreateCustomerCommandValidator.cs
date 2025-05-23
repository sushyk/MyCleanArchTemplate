using FluentValidation;
using Microsoft.Extensions.Logging;

namespace MyCleanArchTemplate.Application.Customers.CreateCustomer;

internal sealed class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator(ILogger<CreateCustomerCommandValidator> logger)
    {
        logger.LogInformation("Validating CreateCustomerCommand");

        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);

        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(50);
    }
}
