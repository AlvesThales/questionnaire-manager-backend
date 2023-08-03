using System.Diagnostics.CodeAnalysis;

namespace QuestionnaireManager.Infrastructure.Utils;

public class Result
{
    public bool Success { get; }
    public string ErrorMessage { get; }

    public bool Failure => !Success;

    protected Result(bool success, string errorMessage)
    {
        Success = success;
        ErrorMessage = errorMessage;
    }

    public static Result Fail(string message)
    {
        return new Result(false, message);
    }

    public static Result<T> Fail<T>(string message)
    {
        return new Result<T>(default, false, message);
    }

    public static Result Ok()
    {
        return new Result(true, string.Empty);
    }

    public static Result<T> Ok<T>(T value)
    {
        return new Result<T>(value, true, string.Empty);
    }

    public static Result Combine(params Result[] results)
    {
        foreach (Result result in results)
        {
            if (result.Failure)
                return result;
        }

        return Ok();
    }
}


public class Result<T> : Result
{
    [AllowNull]
    public T Value {  get; }

    protected internal Result(T? value, bool success, string errorMessage)
        : base(success, errorMessage)
    {
        Value = value;
    }
}