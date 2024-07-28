namespace wf.Api.Middleware.ErrorHandling;

public static class ErrorHandlingPipelineExtensions
{
    public static IApplicationBuilder ConfigureErrorHandling(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}
