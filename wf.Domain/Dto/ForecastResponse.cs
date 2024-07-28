namespace wf.Domain.Dto;

public class ForecastResponse
{
    public DateOnly Date { get; set; }

    public double Temperature { get; set; }

    public double TemperatureFeelLike { get; set; }

    public double Pressure { get; set; }

    public double Humidity { get; set; }

    public string Description { get; set; } = default!;
}
