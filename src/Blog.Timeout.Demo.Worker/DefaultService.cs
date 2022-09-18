using System.Text.Json;
using Polly;
using Polly.Timeout;

namespace Blog.Timeout.Demo.Worker;

public class DefaultService : IDefaultService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<DefaultService> _logger;
    private WeatherSummary _lastSummary = new() { Summary = "Unknown" };
    public DefaultService(HttpClient httpClient, ILogger<DefaultService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<WeatherSummary> GetWeatherSummaryAsync(CancellationToken stoppingToken)
    {
        var timeoutPolicy = Policy.TimeoutAsync<string>(20, TimeoutStrategy.Optimistic);
        try
        {
            var response = await timeoutPolicy.ExecuteAsync(async ct =>
                await _httpClient.GetStringAsync("/WeatherForecast", ct), stoppingToken);

            var weather = JsonSerializer.Deserialize<WeatherSummary>(response,
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

            if (weather != null)
            {
                _lastSummary = weather;
            }
            return _lastSummary;
        }
        catch (TimeoutRejectedException ex)
        {
            _logger.LogWarning("Could not get WeatherSummary before timeout was exceeded. Returning cached summary.");
        }

        return _lastSummary;
    }
}
