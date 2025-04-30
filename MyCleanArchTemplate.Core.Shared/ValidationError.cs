namespace MyCleanArchTemplate.Core.Shared;

public sealed record ValidationError : Error
{
    public ValidationError(Error[] errors) :
        base("Validation.General", "One or more validation errors occured", ErrorType.Validation)
    {
        Errors = errors;
    }

    public Error[] Errors { get; }

    public static ValidationError FromResults(IEnumerable<Result> results) =>
        new(results.Where(r => r.IsFailure).Select(r => r.Error).ToArray());

}
