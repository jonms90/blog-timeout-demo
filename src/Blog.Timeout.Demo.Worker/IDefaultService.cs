public interface IDefaultService
{
    Task<WeatherSummary> GetWeatherSummaryAsync(CancellationToken ct);
}
