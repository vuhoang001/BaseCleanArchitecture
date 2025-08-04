using System.Net;

namespace Shared.ExceptionBase;

/// <summary>
/// Base exception cho toàn bộ lỗi nghiệp vụ hệ thống
/// </summary>
public abstract class BusinessException : Exception
{
    public string ErrorCode { get; }
    public HttpStatusCode StatusCode { get; }

    protected BusinessException(string message, HttpStatusCode statusCode, string errorCode)
        : base(message)
    {
        ErrorCode  = errorCode;
        StatusCode = statusCode;
    }
}

/// <summary>
/// 404 Not Found (API mặc định).
/// </summary>
public class ApiNotFoundException : BusinessException
{
    public ApiNotFoundException(string message = "Không tìm thấy")
        : base(message, HttpStatusCode.NotFound, "BAD_REQUEST")
    {
    }
}

/// <summary>
/// 400 Not Found (API mặc định).
/// </summary>
public class ApiBadRequestException : BusinessException
{
    public ApiBadRequestException(string message = "Đã có lỗi xảy ra")
        : base(message, HttpStatusCode.NotFound, "NOT_FOUND")
    {
    }
}

/// <summary>
/// 401 Unauthorized.
/// </summary>
public class ApiAuthorizationException : BusinessException
{
    public ApiAuthorizationException(string message = "Đã có lỗi xảy ra")
        : base(message, HttpStatusCode.NotFound, "UNAUTHORIZED")
    {
    }
}