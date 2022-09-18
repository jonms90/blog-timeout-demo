public class WeatherSummary
{
    //[JsonPropertyName("date")]
    public DateTime Date { get; set; }

    //[JsonPropertyName("temperatureC")]
    public int TemperatureC { get; set; }
    //[JsonPropertyName("summary")]
    public string? Summary { get; set; }
}
