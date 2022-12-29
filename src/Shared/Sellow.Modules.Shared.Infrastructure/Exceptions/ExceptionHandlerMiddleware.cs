using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Sellow.Modules.Shared.Abstractions.Exceptions;

namespace Sellow.Modules.Shared.Infrastructure.Exceptions;

internal sealed class ExceptionHandlerMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "{Message}", exception.Message);
            await HandleException(context, exception);
        }
    }

    private static async Task HandleException(HttpContext context, Exception exception)
    {
        var errorCode = "server_error";
        var message = "Whops! Something went wrong.";
        var statusCode = HttpStatusCode.InternalServerError;
        
        if (exception is SellowException sellowException)
        {
            errorCode = sellowException.ErrorCode;
            message = sellowException.Message;
            statusCode = sellowException.StatusCode;
        }

        context.Response.StatusCode = (int) statusCode;
        await context.Response.WriteAsJsonAsync(new
        {
            statusCode,
            errorCode,
            message
        });
    }
}