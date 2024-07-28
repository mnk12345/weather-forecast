using System.Text.Json;
using Microsoft.Extensions.Options;
using wf.Domain.Options;

namespace wf.DataAccess.DataAccessors.OpenWeatherMap;

internal interface IOpenWeatherMapRequestExecutor
{
    Task<OpenWeatherMapModel> Execute(OpenWeatherMapRequest request);
}

internal sealed class OpenWeatherMapRequestExecutor(IHttpClientFactory httpClientFactory, IOptions<WeatherApiOptions> weatherOptions) : IOpenWeatherMapRequestExecutor
{
    public async Task<OpenWeatherMapModel> Execute(OpenWeatherMapRequest request)
    {
        var url = GenerateApiLink(request);

        var response = await httpClientFactory.CreateClient().GetAsync(url);

        var content = await response.Content.ReadAsStringAsync();
        var model = JsonSerializer.Deserialize<OpenWeatherMapModel>(content)!;

        return model;
    }

    private string GenerateApiLink(OpenWeatherMapRequest request)
    {
        return $"{weatherOptions.Value.BaseUrl}forecast?q={request.City}&cnt={request.Period}&appid={weatherOptions.Value.ApiKey}";
    }
}
