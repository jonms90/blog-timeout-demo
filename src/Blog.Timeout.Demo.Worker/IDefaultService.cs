namespace Blog.Timeout.Demo.Worker;

public interface IDefaultService
{
    Task<WeatherSummary> GetWeatherSummaryAsync(CancellationToken ct);
}
