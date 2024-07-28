namespace wf.Api.Middleware.Swagger;

public static class SwaggerPipelineExtensions
{
    public static IServiceCollection ConfigureSwagger(this IServiceCollection services, string xmlDocName)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options => { options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{xmlDocName}.xml")); });

        services.ConfigureOptions<VersioningSwaggerGenOptions>();

        return services;
    }

    public static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder appBuilder)
    {
        appBuilder.UseSwagger();

        appBuilder.UseSwaggerUI(c =>
        {
            c.EnableValidator();
            c.EnableDeepLinking();
            c.DisplayRequestDuration();

            var descriptions = ((IEndpointRouteBuilder)appBuilder)
                .DescribeApiVersions()
                .OrderByDescending(x => string.IsNullOrEmpty(x.ApiVersion.Status))
                .ThenByDescending(x => x.ApiVersion);

            foreach (var description in descriptions)
            {
                var url = $"/swagger/{description.GroupName}/swagger.json";
                var name = description.GroupName.ToUpperInvariant();

                c.SwaggerEndpoint(url, name);
            }
        });

        return appBuilder;
    }
}
