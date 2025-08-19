using System.Net;

namespace Shared.ExceptionBase;

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

public class ApiNotFoundException : BusinessException
{
    public ApiNotFoundException(string message = "Không tìm thấy")
        : base(message, HttpStatusCode.NotFound, "NOT_FOUND")
    {
    }
}

public class ApiBadRequestException : BusinessException
{
    public ApiBadRequestException(string message = "Đã có lỗi xảy ra")
        : base(message, HttpStatusCode.BadRequest, "BAD_REQUEST")
    {
    }
}

public class ApiAuthorizationException : BusinessException
{
    public ApiAuthorizationException(string message = "Không có quyền truy cập")
        : base(message, HttpStatusCode.Unauthorized, "UNAUTHORIZED")
    {
    }
}

public class ApiConflictException : BusinessException
{
    public ApiConflictException(string message = "Dữ liệu đã tồn tại")
        : base(message, HttpStatusCode.Conflict, "CONFLICT")
    {
    }
}

public class ApiInternalServerException : BusinessException
{
    public ApiInternalServerException(string message = "Lỗi hệ thống")
        : base(message, HttpStatusCode.InternalServerError, "INTERNAL_ERROR")
    {
    }
}