using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Retry;
using Polly.Timeout;
using wf.DataAccess.DataAccessors.OpenWeatherMap;
using wf.Domain.DataAccessors;

namespace wf.DataAccess;

public static class Bootstrapper
{
    public static void BootstrapDataAccess(this IServiceCollection services)
    {
        services.AddHttpClient().AddResiliencePipeline("wf-pipeline", builder =>
        {
            builder.AddRetry(new RetryStrategyOptions
            {
                ShouldHandle = new PredicateBuilder()
                    .Handle<TimeoutRejectedException>()
                    .Handle<HttpRequestException>(),

                MaxRetryAttempts = 4,
                Delay = TimeSpan.FromSeconds(2),
                BackoffType = DelayBackoffType.Exponential,
                UseJitter = true
            }).AddTimeout(TimeSpan.FromSeconds(2));
        });

        services.AddScoped<IOpenWeatherMapModelConverter, OpenWeatherMapModelConverter>();
        services.AddScoped<IOpenWeatherMapRequestExecutor, OpenWeatherMapRequestExecutor>();
        services.AddScoped<IForecastAccessor, OpenWeatherMapAccessor>();
    }
}
