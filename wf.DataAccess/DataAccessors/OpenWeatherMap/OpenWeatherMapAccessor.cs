using wf.Domain.DataAccessors;
using wf.Domain.Dto;
using wf.Domain.Exceptions;

namespace wf.DataAccess.DataAccessors.OpenWeatherMap;

internal sealed class OpenWeatherMapAccessor(IOpenWeatherMapRequestExecutor openWeatherMapRequestExecutor, IOpenWeatherMapModelConverter openWeatherMapModelConverter) : IForecastAccessor
{
    public async Task<ForecastResponse?> GetForecast(ForecastRequest forecastRequest)
    {
        var openWeatherMapRequest = openWeatherMapModelConverter.Convert(forecastRequest);

        var openWeatherMapResponseModel = await openWeatherMapRequestExecutor.Execute(openWeatherMapRequest);

        if (openWeatherMapResponseModel.Code != "200")
        {
            throw new ValidationWfException(openWeatherMapResponseModel.Message?.ToString() ?? "OWM request failed", openWeatherMapResponseModel.Code);
        }

        return openWeatherMapModelConverter.Convert(openWeatherMapResponseModel, forecastRequest);
    }
}
