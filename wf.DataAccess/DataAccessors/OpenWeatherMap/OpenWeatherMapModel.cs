using System.Text.Json.Serialization;

namespace wf.DataAccess.DataAccessors.OpenWeatherMap;

internal sealed class OpenWeatherMapModel
{
    [JsonPropertyName("cod")]
    public string? Code { get; set; }

    [JsonPropertyName("message")]
    public object? Message { get; set; }

    [JsonPropertyName("cnt")]
    public int Day { get; set; }

    [JsonPropertyName("city")]
    public OpenWeatherMapForecastCity City { get; set; } = default!;

    [JsonPropertyName("list")]
    public OpenWeatherMapForecastItem[] List { get; set; } = [];
}

internal sealed class OpenWeatherMapForecastItem
{
    [JsonPropertyName("dt")]
    public int Day { get; set; }

    [JsonPropertyName("main")]
    public OpenWeatherMapForecastItemMain Main { get; set; } = default!;

    [JsonPropertyName("weather")]
    public OpenWeatherMapForecastItemWeather[] Weather { get; set; } = [];

    [JsonPropertyName("wind")]
    public OpenWeatherMapForecastItemWind Wind { get; set; } = default!;
}

internal sealed class OpenWeatherMapForecastItemMain
{
    [JsonPropertyName("temp")]
    public double Temp { get; set; }

    [JsonPropertyName("feels_like")]
    public double FeelsLike { get; set; }

    [JsonPropertyName("temp_min")]
    public double TempMin { get; set; }

    [JsonPropertyName("temp_max")]
    public double TempMax { get; set; }

    [JsonPropertyName("pressure")]
    public double Pressure { get; set; }

    [JsonPropertyName("humidity")]
    public double Humidity { get; set; }
}

internal sealed class OpenWeatherMapForecastItemWeather
{
    [JsonPropertyName("main")]
    public string Main { get; set; } = default!;

    [JsonPropertyName("description")]
    public string Description { get; set; } = default!;
}

internal sealed class OpenWeatherMapForecastItemWind
{
    [JsonPropertyName("speed")]
    public double Speed { get; set; }

    [JsonPropertyName("deg")]
    public double Deg { get; set; }
}

internal sealed class OpenWeatherMapForecastCity
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;
}
