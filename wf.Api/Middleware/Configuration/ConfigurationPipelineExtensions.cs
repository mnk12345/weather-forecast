using wf.Domain.Options;

namespace wf.Api.Middleware.Configuration;

public static class ConfigurationPipelineExtensions
{
    public static IServiceCollection ConfigureEnvOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<WeatherApiOptions>(configuration.GetSection("WeatherApiOptions"));
        services.AddOptions<WeatherApiOptions>().ValidateFluently().ValidateOnStart();

        return services;
    }
}
