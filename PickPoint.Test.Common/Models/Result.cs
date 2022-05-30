using PickPoint.Test.Common.Enums;

namespace PickPoint.Test.Common.Models;

public record Result
{
    public bool IsSuccess => ErrorCode == ErrorCode.NoError;

    public ErrorCode ErrorCode { get; private set; }

    public string Message { get; private set; }

    public Result()
    {

    }

    public Result(ErrorCode errorCode, string message)
    {
        ErrorCode = errorCode;
        Message = message;
    }

    public void SerError(ErrorCode errorCode, string message)
    {
        ErrorCode = errorCode;
        Message = message;
    }

    public static Result Empty => new();
}

public record Result<T> : Result
{
    public T Data { get; private set; }

    public Result()
    {

    }

    public Result(T data) : base(ErrorCode.NoError, default)
    {
        Data = data;
    }

    public Result(T data, ErrorCode errorCode, string message) : base(errorCode, message)
    {
        Data = data;
    }
}
