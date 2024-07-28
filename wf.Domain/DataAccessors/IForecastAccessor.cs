using wf.Domain.Dto;

namespace wf.Domain.DataAccessors;

public interface IForecastAccessor
{
    public Task<ForecastResponse?> GetForecast(ForecastRequest request);
}
