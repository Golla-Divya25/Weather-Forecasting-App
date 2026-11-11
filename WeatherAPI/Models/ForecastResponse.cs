
namespace WeatherAPI.Models
{
    public class ForecastResponse
    {
        public string City { get; set; } = string.Empty;

        public List<ForecastDay> Days { get; set; } = new();
    }

    public class ForecastDay
    {
        public DateTime Date { get; set; }

        public double TemperatureC { get; set; }

        public string Description { get; set; } = string.Empty;

        public string Icon { get; set; } = string.Empty;
    }
}
