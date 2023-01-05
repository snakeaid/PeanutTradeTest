using System.Net;
using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace PeanutTradeTest.Middleware;

public class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = context.Response;
        response.ContentType = "application/json";

        switch (exception)
        {
            case ArgumentException:
            case NullReferenceException:
            case ValidationException:
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                break;
            default:
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                break;
        }
        
        _logger.LogError(exception.Message);

        await response.WriteAsync(JsonSerializer.Serialize(new
            { errorMessage = exception.Message }, new JsonSerializerOptions { WriteIndented = true }));
    }
}