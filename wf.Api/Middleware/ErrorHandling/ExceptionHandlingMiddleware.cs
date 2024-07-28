using System.Text.Json;
using FluentValidation;
using wf.Domain.Exceptions;

namespace wf.Api.Middleware.ErrorHandling;

internal sealed class ExceptionHandlingMiddleware(RequestDelegate next, IHostEnvironment hostEnvironment, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception ex)
        {
            httpContext.Response.Clear();
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = GetStatusCode(ex);

            var response = CreateResponse(ex);

            await httpContext.Response.WriteAsync(response);

            LogException(ex);
        }
    }

    private string CreateResponse(Exception ex)
    {
        var response = new WfErrorResponse
        {
            ErrorCode = GetErrorCode(ex),
            ErrorMessage = hostEnvironment.IsDevelopment() ? ex.Message : null,
            ErrorStackTrace = hostEnvironment.IsDevelopment() ? ex.StackTrace : null
        };

        return JsonSerializer.Serialize(response);
    }

    private static int GetStatusCode(Exception ex)
    {
        return ex switch
        {
            ValidationException => StatusCodes.Status400BadRequest,
            ValidationWfException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };
    }

    private static string GetErrorCode(Exception ex)
    {
        return ex switch
        {
            _ => GetStatusCode(ex).ToString()
        };
    }

    private void LogException(Exception ex)
    {
        logger.LogError(ex, "Unhandled exception has been occurred!");
    }
}
