using wf.Domain.Common;
using wf.Domain.Dto;

namespace wf.DataAccess.DataAccessors.OpenWeatherMap;

internal interface IOpenWeatherMapModelConverter
{
    OpenWeatherMapRequest Convert(ForecastRequest request);
    ForecastResponse? Convert(OpenWeatherMapModel model, ForecastRequest request);
}

internal sealed class OpenWeatherMapModelConverter(IDateTimeProvider dateTimeProvider) : IOpenWeatherMapModelConverter
{
    public OpenWeatherMapRequest Convert(ForecastRequest request)
    {
        var period = (int)(request.Date.ToDateTime(TimeOnly.MinValue) - dateTimeProvider.UtcNow).TotalDays * 8 + 8;
        return new OpenWeatherMapRequest { City = request.City, Period = period };
    }

    public ForecastResponse? Convert(OpenWeatherMapModel model, ForecastRequest request)
    {
        var result = model.List.LastOrDefault(x => FromEpoch(x.Day) == request.Date);

        if (result is null)
        {
            return null;
        }

        return new ForecastResponse
        {
            Date = FromEpoch(result.Day),
            Temperature = ToCelsius(result.Main.Temp),
            TemperatureFeelLike = ToCelsius(result.Main.FeelsLike),
            Pressure = result.Main.Pressure,
            Humidity = result.Main.Humidity,
            Description = result.Weather[0].Main
        };
    }

    // TODO: move to common extension
    private static DateOnly FromEpoch(long unixTime)
    {
        return DateOnly.FromDateTime(DateTime.UnixEpoch.AddSeconds(unixTime));
    }

    // TODO: move to common extension
    private static double ToCelsius(double temp)
    {
        return Math.Round(temp - 273.15, 1);
    }
}
