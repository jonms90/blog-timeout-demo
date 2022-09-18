namespace Blog.Timeout.Demo.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IDefaultService _defaultService;

        public Worker(ILogger<Worker> logger, IDefaultService defaultService)
        {
            _logger = logger;
            _defaultService = defaultService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    var weather = await _defaultService.GetWeatherSummaryAsync(stoppingToken);
                    _logger.LogInformation("Weather summary received: {degrees}: {summary}", weather.TemperatureC, weather.Summary);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unhandled exception thrown during execution.");
                }
                finally
                {
                    await Task.Delay(5000, stoppingToken);
                }
            }
        }
    }
}
