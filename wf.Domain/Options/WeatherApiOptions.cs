namespace wf.Domain.Options;

public sealed class WeatherApiOptions
{
    public string ApiKey { get; init; } = default!;
    public string BaseUrl { get; init; } = default!;
}
