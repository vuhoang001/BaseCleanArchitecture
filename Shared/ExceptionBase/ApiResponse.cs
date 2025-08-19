namespace Shared.ExceptionBase;

using System.Text.Json.Serialization;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string? Message { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public T Data { get; set; }


    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string ErrorCode { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string> Errors { get; set; }

    public static ApiResponse<T> CreateSuccess(T data, string message = "Thành công")
    {
        return new ApiResponse<T>
        {
            Success = true,
            Data    = data,
            Message = message
        };
    }

    public static ApiResponse<T> CreateFailure(string message, string errorCode = null, List<string> errors = null)
    {
        return new ApiResponse<T>
        {
            Success   = false,
            Message   = message,
            ErrorCode = errorCode,
            Errors    = errors?.Count > 1 ? errors : []
        };
    }
}

public class ApiResponse : ApiResponse<object>
{
    public static ApiResponse CreateSuccess(string message = "Thành công")
    {
        return new ApiResponse
        {
            Success = true,
            Message = message
        };
    }

    public static new ApiResponse CreateFailure(string message, string errorCode = null, List<string> errors = null)
    {
        return new ApiResponse
        {
            Success   = false,
            Message   = message,
            ErrorCode = errorCode,
            Errors    = errors?.Count >= 1 ? errors : []
        };
    }
}

public static class ResultExtensions
{
    public static ApiResponse<T> ToApiResponse<T>(this Result<T> result, string successMessage = null)
    {
        if (result.IsSuccess)
        {
            return ApiResponse<T>.CreateSuccess(result.Data, successMessage ?? "Thành công");
        }

        return ApiResponse<T>.CreateFailure(
            result.Error,
            result.ErrorCode,
            result.Errors
        );
    }

    public static ApiResponse ToApiResponse(this Result result, string successMessage = null)
    {
        if (result.IsSuccess)
        {
            return ApiResponse.CreateSuccess(successMessage ?? "Thành công");
        }

        return ApiResponse.CreateFailure(
            result.Error,
            result.ErrorCode,
            result.Errors
        );
    }
}