using System.Net;
using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace PeanutTradeTest.Middleware;

/// <summary>
/// Middleware class used to handle exceptions.
/// </summary>
public class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    
    /// <summary>
    /// Constructs an instance of <see cref="ExceptionHandlingMiddleware"/> using the specified logger.
    /// </summary>
    /// <param name="logger">An instance of <see cref="ILogger{TCategoryName}"/> for <see cref="ExceptionHandlingMiddleware"/>.</param>
    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Tries to invoke the delegate and catches the exception if there is one.
    /// </summary>
    /// <param name="context">An instance of <see cref="HttpContext"/> class.</param>
    /// <returns><see cref="Task"/></returns>
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