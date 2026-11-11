
using System.Text.Json.Serialization;

namespace WeatherAPI.Models
{
    // Model for the current weather API response
    public class OwmCurrentResponse
    {
        [JsonPropertyName("name")]
        public string CityName { get; set; } = string.Empty;

        [JsonPropertyName("main")]
        public OwmMain Main { get; set; } = new();

        [JsonPropertyName("weather")]
        public List<OwmWeatherDescription> Weather { get; set; } = new();

        [JsonPropertyName("wind")]
        public OwmWind Wind { get; set; } = new();

        [JsonPropertyName("sys")]
        public OwmSys Sys { get; set; } = new();
    }

    public class OwmMain
    {
        [JsonPropertyName("temp")]
        public double Temp { get; set; }

        [JsonPropertyName("feels_like")]
        public double FeelsLike { get; set; }

        [JsonPropertyName("humidity")]
        public int Humidity { get; set; }

        [JsonPropertyName("pressure")]
        public int Pressure { get; set; }
    }

    public class OwmWeatherDescription
    {
        [JsonPropertyName("main")]
        public string Main { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("icon")]
        public string Icon { get; set; } = string.Empty;
    }

    public class OwmWind
    {
        [JsonPropertyName("speed")]
        public double Speed { get; set; }
    }

    public class OwmSys
    {
        [JsonPropertyName("country")]
        public string Country { get; set; } = string.Empty;
    }

    // Model for the forecast API response
    public class OwmForecastResponse
    {
        [JsonPropertyName("list")]
        public List<OwmForecastItem> List { get; set; } = new();

        [JsonPropertyName("city")]
        public OwmCity City { get; set; } = new();
    }

    public class OwmForecastItem
    {
        [JsonPropertyName("dt_txt")]
        public string DateTimeText { get; set; } = string.Empty;

        [JsonPropertyName("main")]
        public OwmMain Main { get; set; } = new();

        [JsonPropertyName("weather")]
        public List<OwmWeatherDescription> Weather { get; set; } = new();
    }

    public class OwmCity
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
    }
}
