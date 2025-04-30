namespace MyCleanArchTemplate.Core.Shared
{
    public class Result
    {
        public Result(bool isSuccess, Error error)
        {
            if (isSuccess && error.Type == ErrorType.Failure)
            {
                throw new ArgumentException("Error should be None if Result is Success", nameof(error));
            }

            if (!isSuccess && error.Type == ErrorType.None)
            {
                throw new ArgumentException("Error should not be None if Result is Failure", nameof(error));
            }

            IsSuccess = isSuccess;
            Error = error;
        }

        public bool IsSuccess { get; set; }

        public bool IsFailure => !IsSuccess;

        public Error Error { get; }

        public static Result Success() => new(true, Error.None);

        public static Result<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);

        public static Result Failure(Error error) => new(false, error);

        public static Result<TValue> Failure<TValue>(Error error) => new(default, false, error);
    }

    public class Result<TValue> : Result
    {
        public TValue? Value { get; private set; }

        public Result(TValue value, bool isSuccess, Error error) : base(isSuccess, error)
        {
            Value = value;
        }
    }
}