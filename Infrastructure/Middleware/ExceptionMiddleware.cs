using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Shared.ExceptionBase;

namespace Infrastructure.Middleware;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException ex)
        {
            logger.LogWarning(ex.Message);

            var errors = ex.Errors
                .GroupBy(e => e.PropertyName)
                .SelectMany(g => g.Select(e => e.ErrorMessage))
                .ToList();

            var response = ApiResponse.CreateFailure("Lỗi dữ liệu đầu vào", "VALIDATION_ERROR", errors);

            await WriteErrorResponse(context, HttpStatusCode.BadRequest, response.ErrorCode, response.Message!,
                response.Errors);
        }
        catch (BusinessException ex)
        {
            logger.LogWarning("Business error: {Message}", ex.Message);
            await WriteErrorResponse(context, ex.StatusCode, ex.ErrorCode, ex.Message);
        }
        catch (UnauthorizedAccessException ex)
        {
            await WriteErrorResponse(context, HttpStatusCode.Unauthorized, "UNAUTHORIZE", ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception");
            await WriteErrorResponse(context,
                HttpStatusCode.InternalServerError,
                "INTERNAL_SERVER_ERROR",
                "Đã xảy ra lỗi hệ thống", [ex.Message]);
        }
    }

    private static async Task WriteErrorResponse(HttpContext context,
        HttpStatusCode statusCode,
        string errorCode,
        string message,
        List<string>? errors = null)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode  = (int)statusCode;

        var response = ApiResponse.CreateFailure(message, errorCode, errors);

        var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy   = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        });

        await context.Response.WriteAsync(json);
    }
}