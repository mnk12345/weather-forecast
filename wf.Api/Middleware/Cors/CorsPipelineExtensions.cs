namespace wf.Api.Middleware.Cors;

public static class CorsPipelineExtensions
{
    public static IServiceCollection ConfigureCors(this IServiceCollection services)
    {
        services.AddCors();
        return services;
    }

    public static IApplicationBuilder ConfigureCors(this IApplicationBuilder appBuilder)
    {
        appBuilder.UseCors(builder =>
        {
            builder.WithOrigins(["https://localhost:7172"])
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
        return appBuilder;
    }
}
