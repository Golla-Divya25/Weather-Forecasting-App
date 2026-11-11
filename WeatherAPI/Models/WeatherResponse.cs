
namespace WeatherAPI.Models
{
    public class WeatherResponse
    {
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

        public double TemperatureC { get; set; }
        public double FeelsLikeC { get; set; }

        public int Humidity { get; set; }
        public int Pressure { get; set; }

        public double WindSpeed { get; set; }

        public string Description { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;

        public DateTime RetrievedAtUtc { get; set; }
    }
}
