using System.Net;
using System.Text.Json;
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
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );

            await WriteErrorResponse(context, HttpStatusCode.BadRequest, "VALIDATION_ERROR", "Lỗi dữ liệu đầu vào!",
                errors);
        }
        catch (BusinessException ex) // Lỗi BusinessException custom
        {
            logger.LogWarning("Business error: {Message}", ex.Message);
            await WriteErrorResponse(context, ex.StatusCode, ex.ErrorCode, ex.Message);
        }
        catch (Exception ex) // Lỗi hệ thống
        {
            logger.LogError(ex, "Unhandled exception");
            await WriteErrorResponse(context, HttpStatusCode.InternalServerError, "INTERNAL_SERVER_ERROR", ex.Message);
        }
    }

    private static async Task WriteErrorResponse(HttpContext context, HttpStatusCode statusCode, string errorCode,
        string message, object? details = null)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode  = (int)statusCode;

        var result = new
        {
            StatusCode = context.Response.StatusCode,
            ErrorCode  = errorCode,
            Message    = message,
            Details    = details
        };

        var json = JsonSerializer.Serialize(result);
        await context.Response.WriteAsync(json);
    }
}