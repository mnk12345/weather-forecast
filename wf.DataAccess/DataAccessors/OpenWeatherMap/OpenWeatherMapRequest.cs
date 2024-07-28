namespace wf.DataAccess.DataAccessors.OpenWeatherMap;

internal sealed class OpenWeatherMapRequest
{
    public string City { get; set; } = default!;

    public int Period { get; set; }
}
