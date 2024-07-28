using wf.Domain.Dto;

namespace wf.Domain.Services;

public interface IForecastService
{
    public Task<ForecastResponse?> GetForecast(ForecastRequest request);
}
