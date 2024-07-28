using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace wf.Api.Middleware.Swagger;

internal sealed class VersioningSwaggerGenOptions(IApiVersionDescriptionProvider provider) : IConfigureNamedOptions<SwaggerGenOptions>
{
    private const string Title = "Weather Forecast API";

    public void Configure(string? name, SwaggerGenOptions options)
    {
        Configure(options);
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateVersionOpenApiInfo(description));
        }
    }

    private static OpenApiInfo CreateVersionOpenApiInfo(ApiVersionDescription description)
    {
        var info = new OpenApiInfo
        {
            Title = $"{Title} v{description.ApiVersion.MajorVersion}",
            Version = description.ApiVersion.ToString()
        };

        if (description.IsDeprecated)
        {
            info.Description += " Warning: This API version has been deprecated!";
        }

        if (!string.IsNullOrEmpty(description.ApiVersion.Status))
        {
            info.Description += $" Warning: This API version is in {description.ApiVersion.Status} status!";
        }

        return info;
    }
}
