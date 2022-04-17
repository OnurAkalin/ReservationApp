using System.ComponentModel.DataAnnotations;
using System.Security;
using Microsoft.AspNetCore.Http;

namespace Core.Extensions;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(httpContext, exception);
        }
    }


    private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        string message;
        
        if (exception.GetType() == typeof(ValidationException))
        {
            message = exception.Message;
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
        else if (exception.GetType() == typeof(ApplicationException))
        {
            message = exception.Message;
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
        else if (exception.GetType() == typeof(UnauthorizedAccessException))
        {
            message = exception.Message;
            httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
        }
        else if (exception.GetType() == typeof(SecurityException))
        {
            message = exception.Message;
            httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
        }
        else if (exception.GetType() == typeof(NotSupportedException))
        {
            message = exception.Message;
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
        else
        {
            message = "Something went wrong please try again.";
        }

        await httpContext.Response.WriteAsync(message);
    }
}