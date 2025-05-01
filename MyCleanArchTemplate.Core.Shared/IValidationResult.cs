namespace MyCleanArchTemplate.Core.Shared;

public interface IValidationResult
{
    static readonly Error ValidationError = Error.Validation("ValidationError", "A validation error occurred.");

    Error[] Errors { get; }
}
