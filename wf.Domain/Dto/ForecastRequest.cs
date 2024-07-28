namespace wf.Domain.Dto;

public class ForecastRequest
{
    public string City { get; set; } = default!;

    public DateOnly Date { get; set; }
}
