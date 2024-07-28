using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using wf.Domain.Dto;
using wf.Domain.Services;

namespace wf.Api.Controllers;

[ApiVersion(1.0)]
[ApiController]
[Route("api/v{version:apiVersion}")]
public class WeatherForecastController(IForecastService forecastService) : ControllerBase
{
    /// <summary>
    /// Get weather forecast.
    /// </summary>
    /// <param name="request">Forecast request with specified city and date.</param>
    /// <returns>Forecast details.</returns>
    [HttpGet]
    [Route("forecast")]
    public Task<ForecastResponse?> Get([FromQuery] ForecastRequest request)
    {
        return forecastService.GetForecast(request);
    }
}
