using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace BenchlyBackend.Middleware;

public class GlobalErrorHandler(RequestDelegate next, ILogger<GlobalErrorHandler> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<GlobalErrorHandler> _logger = logger;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unhandled exception occured");
            await HandleExceptionAsync(context, e);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        int code = exception switch
        {
            ArgumentException => code = StatusCodes.Status400BadRequest,
            UnauthorizedAccessException => code = StatusCodes.Status401Unauthorized,
            DbUpdateException => code = StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };

        var result = JsonSerializer.Serialize(new
        {
            errorMessage = exception.Message,
            status = code,
            path = context.Request.Path,
            traceId = context.TraceIdentifier
        });

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = code;

        return context.Response.WriteAsync(result);
    }
}