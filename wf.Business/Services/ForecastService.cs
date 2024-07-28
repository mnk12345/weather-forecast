using FluentValidation;
using wf.Domain.Common;
using wf.Domain.DataAccessors;
using wf.Domain.Dto;
using wf.Domain.Services;

namespace wf.Business.Services;

internal sealed class ForecastService(ICacheService cacheService, IForecastAccessor forecastAccessor, IValidator<ForecastRequest> forecastRequestValidator) : IForecastService
{
    private static readonly TimeSpan CacheExpireIn = TimeSpan.FromMinutes(20);

    public async Task<ForecastResponse?> GetForecast(ForecastRequest request)
    {
        await forecastRequestValidator.ValidateAndThrowAsync(request);

        var result = await cacheService.GetOrSet(
            GetCacheKey(request),
            () => forecastAccessor.GetForecast(request),
            CacheExpireIn
        );

        return result;
    }

    private static string GetCacheKey(ForecastRequest request)
    {
        return $"{request.City}_{request.Date}";
    }
}
