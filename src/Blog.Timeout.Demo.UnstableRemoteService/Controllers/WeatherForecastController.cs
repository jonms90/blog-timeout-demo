using Microsoft.AspNetCore.Mvc;

namespace Blog.Timeout.Demo.UnstableRemoteService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly Random _random;
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _random = new Random();
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<WeatherForecast> Get()
        {
            _logger.LogDebug("Getting a request for a weatherforecast at: {time}", DateTimeOffset.Now);

            if (_random.Next(0, 2) == 0)
            {
                await Task.Delay(TimeSpan.FromMinutes(2));
            }

            return new WeatherForecast
            {
                Date = DateTime.Now.AddDays(1),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            };
        }
    }
}
