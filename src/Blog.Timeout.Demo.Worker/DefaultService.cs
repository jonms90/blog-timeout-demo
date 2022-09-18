using System.Text.Json;
using System.Text.Json.Serialization;

namespace Blog.Timeout.Demo.Worker;

public class DefaultService : IDefaultService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<DefaultService> _logger;
    public DefaultService(HttpClient httpClient, ILogger<DefaultService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<WeatherSummary> GetWeatherSummaryAsync()
    {
        var result = await _httpClient.GetStringAsync("/WeatherForecast");
        var weather =
            JsonSerializer.Deserialize<WeatherSummary>(result, new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase}) ??

    new WeatherSummary {Summary = "Unknown"};
        return weather;
    }
}