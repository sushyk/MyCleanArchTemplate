namespace MyCleanArchTemplate.Core.Shared;

public interface IValidationResult
{
    static readonly Error ValidationError = new("ValidationError", "A validation error occurred.", ErrorType.Validation);

    Error[] Errors { get; }
}
