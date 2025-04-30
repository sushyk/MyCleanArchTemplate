namespace MyCleanArchTemplate.Core.Shared;

public class Result
{
    private Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None ||
            !isSuccess && error == Error.None)
        {
            throw new ArgumentNullException(nameof(error), "Invalid error");
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; set; }

    public bool IsFailure => !IsSuccess;

    public Error Error { get; }

    public static Result Success() => new(true, Error.None);

    public static Result Failure(Error error) => new(false, error);
}
