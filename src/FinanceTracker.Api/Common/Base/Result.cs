namespace FinanceTracker.Api.Common.Base;

public class Result
{
    protected Result()
    {
        IsSuccess = true;
    }

    protected Result(Error error)
    {
        IsSuccess = false;
        Error = error;
    }

    public bool IsSuccess { get; private set; }
    public Error? Error { get; private set; }

    public static Result Success() => new();
    public static Result Failure(Error error) => new(error);
}

public class Result<TData> : Result
{
    protected Result(TData data) : base()
    {
        Data = data;
    }

    protected Result(Error error) : base(error) { }
    public TData Data { get; private set; } = default!;

    public static Result<TData> Success(TData data) => new(data);
    public static new Result<TData> Failure(Error error) => new(error);
}