namespace Results;

public class Result<T> : Result
{
    private Result()
    {
    }

    public T? Value { get; init; }

    public new static Result<T> Success(T value)
    {
        return new Result<T> { Value = value };
    }

    public new static Result<T> Failure(params string[] errors)
    {
        return new Result<T> { Errors = errors };
    }

    public static implicit operator Result<T>(T value)
    {
        return Success(value);
    }
}

public class Result
{
    protected Result()
    {
    }

    public bool IsSuccess => Errors == null;

    public string[] Errors { get; init; }

    public static Result Success()
    {
        return new Result();
    }

    public static Result Failure(params string[] errors)
    {
        return new Result { Errors = errors };
    }
}