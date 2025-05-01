using FluentValidation;
using Mediator;
using MyCleanArchTemplate.Core.Shared;

namespace MyCleanArchTemplate.Application.Behaviours;

public class ValidationPipelineBehavior<TMessage, TResponse> : IPipelineBehavior<TMessage, TResponse>
    where TMessage : IMessage
    where TResponse : Result
{
    private readonly IEnumerable<IValidator<TMessage>> _validators;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TMessage>> validators)
    {
        _validators = validators;
    }

    public async ValueTask<TResponse> Handle(TMessage request, MessageHandlerDelegate<TMessage, TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next(request, cancellationToken);
        }

        Error[] errors = [.. _validators
            .Select(validator => validator.Validate(request))
            .SelectMany(validationResult => validationResult.Errors)
            .Where(validationFailure => validationFailure is not null)
            .Select(failure => Error.Validation(failure.PropertyName, failure.ErrorMessage))
            .Distinct()];

        if (errors.Length > 0)
        {
            return CreateValidationResult<TResponse>(errors);
        }

        return await next(request, cancellationToken);
    }

    private static TResult CreateValidationResult<TResult>(Error[] errors) where TResult : Result
    {
        if (typeof(TResult) == typeof(Result))
        {
            return (ValidationResult.WithErrors(errors) as TResult)!;
        }

        object validationResult = typeof(ValidationResult<>)
            .GetGenericTypeDefinition()
            .MakeGenericType(typeof(TResult).GenericTypeArguments[0])
            .GetMethod(nameof(ValidationResult.WithErrors))!
            .Invoke(null, [errors])!;

        return (TResult)validationResult;
    }
}
