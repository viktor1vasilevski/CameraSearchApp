using Application.Enums;
using Application.Responses;
using Infrastructure.Exceptions;
using System.Net;
using System.Text.Json;

namespace WebAPI.Middlewares;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (UnsupportedEntityException ex)
        {
            _logger.LogWarning(ex, "Unsupported entity type.");
            context.Response.StatusCode = (int)HttpStatusCode.NotImplemented;
            context.Response.ContentType = "application/json";

            var jsonResponse = CreateJsonResponse(ex.Message, NotificationType.ServerError);
            await context.Response.WriteAsync(jsonResponse);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception caught in global middleware.");
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var jsonResponse = CreateJsonResponse("An unexpected error occurred. Please try again later.", NotificationType.ServerError);
            await context.Response.WriteAsync(jsonResponse);
        }
    }

    private static string CreateJsonResponse(string message, NotificationType type)
    {
        var response = new ApiResponse<object>
        {
            Success = false,
            Message = message,
            NotificationType = type,
            Data = null
        };

        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        return JsonSerializer.Serialize(response, options);
    }
}
