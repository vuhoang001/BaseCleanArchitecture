using System.Net;
using System.Text.Json.Serialization;

namespace Shared.ExceptionBase;

public class Result
{
    public bool IsSuccess { get; protected set; }
    public bool IsFailure => !IsSuccess;
    public string Error { get; protected set; }
    public List<string> Errors { get; protected set; } = new List<string>();
    public string ErrorCode { get; protected set; }
    public HttpStatusCode StatusCode { get; protected set; }

    protected Result(bool isSuccess, string error = null, string errorCode = null,
        HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        IsSuccess  = isSuccess;
        Error      = error;
        ErrorCode  = errorCode;
        StatusCode = statusCode;

        if (!string.IsNullOrEmpty(error))
            Errors.Add(error);
    }

    protected Result(bool isSuccess, List<string> errors, string errorCode = null,
        HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        IsSuccess  = isSuccess;
        Errors     = errors ?? new List<string>();
        Error      = errors?.FirstOrDefault();
        ErrorCode  = errorCode;
        StatusCode = statusCode;
    }

    public static Result Success() => new Result(true);

    public static Result Failure(string error, string errorCode = null,
        HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        => new Result(false, error, errorCode, statusCode);

    public static Result Failure(List<string> errors, string errorCode = null,
        HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        => new Result(false, errors, errorCode, statusCode);

    // Convert từ BusinessException thành Result
    public static Result FromException(BusinessException ex)
        => new Result(false, ex.Message, ex.ErrorCode, ex.StatusCode);
}

public class Result<T> : Result
{
    public T Data { get; private set; }

    private Result(bool isSuccess, T data, string error = null, string errorCode = null,
        HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        : base(isSuccess, error, errorCode, statusCode)
    {
        Data = data;
    }

    private Result(bool isSuccess, T data, List<string> errors, string errorCode = null,
        HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        : base(isSuccess, errors, errorCode, statusCode)
    {
        Data = data;
    }

    public static Result<T> Success(T data) => new Result<T>(true, data);

    public static new Result<T> Failure(string error, string? errorCode = null,
        HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        => new Result<T>(false, default(T), error, errorCode, statusCode);

    public static new Result<T> Failure(List<string> errors, string? errorCode = null,
        HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        => new Result<T>(false, default(T), errors, errorCode, statusCode);

    // Convert từ BusinessException thành Result<T>
    public static new Result<T> FromException(BusinessException ex)
        => new Result<T>(false, default(T), ex.Message, ex.ErrorCode, ex.StatusCode);
}