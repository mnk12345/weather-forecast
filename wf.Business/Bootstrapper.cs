using Microsoft.Extensions.DependencyInjection;
using wf.Business.Common;
using wf.Business.Services;
using wf.Domain.Common;
using wf.Domain.Services;

namespace wf.Business;

public static class Bootstrapper
{
    public static void BootstrapBusiness(this IServiceCollection services)
    {
        services.AddMemoryCache();

        services.AddScoped<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<ICacheService, MemoryCacheService>();

        services.AddScoped<IForecastService, ForecastService>();
    }
}
