
using System.Net.Http.Json;
using Microsoft.Extensions.Caching.Memory;
using WeatherAPI.Models;

namespace WeatherAPI.Services
{
    public class WeatherService : IWeatherServices
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _cache;

        public WeatherService(
            HttpClient httpClient,
            IConfiguration configuration,
            IMemoryCache cache)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _cache = cache;
        }

        public async Task<WeatherResponse?> GetCurrentWeatherAsync(string city)
        {
            string cacheKey = $"current_{city.ToLower()}";

            if (_cache.TryGetValue(cacheKey, out WeatherResponse? cached))
            {
                return cached;
            }

            string apiKey = _configuration["OpenWeatherMap:ApiKey"]!;
            string baseUrl = _configuration["OpenWeatherMap:BaseUrl"]!;

            string url = $"{baseUrl}/weather?q={Uri.EscapeDataString(city)}&appid={apiKey}&units=metric";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var data = await response.Content
                .ReadFromJsonAsync<OwmCurrentResponse>();

            if (data == null)
            {
                return null;
            }

            var result = new WeatherResponse
            {
                City = data.CityName,
                Country = data.Sys.Country,
                TemperatureC = Math.Round(data.Main.Temp, 1),
                FeelsLikeC = Math.Round(data.Main.FeelsLike, 1),
                Humidity = data.Main.Humidity,
                Pressure = data.Main.Pressure,
                WindSpeed = data.Wind.Speed,
                Description = data.Weather.FirstOrDefault()?.Description ?? "N/A",
                Icon = data.Weather.FirstOrDefault()?.Icon ?? "01d",
                RetrievedAtUtc = DateTime.UtcNow
            };

            _cache.Set(cacheKey, result, TimeSpan.FromMinutes(10));

            return result;
        }

        public async Task<ForecastResponse?> GetForecastAsync(string city)
        {
            string cacheKey = $"forecast_{city.ToLower()}";

            if (_cache.TryGetValue(cacheKey, out ForecastResponse? cached))
            {
                return cached;
            }

            string apiKey = _configuration["OpenWeatherMap:ApiKey"]!;
            string baseUrl = _configuration["OpenWeatherMap:BaseUrl"]!;

            string url = $"{baseUrl}/forecast?q={Uri.EscapeDataString(city)}&appid={apiKey}&units=metric";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var data = await response.Content
                .ReadFromJsonAsync<OwmForecastResponse>();

            if (data == null)
            {
                return null;
            }

            var days = data.List
                .Where(item => item.DateTimeText.Contains("12:00:00"))
                .Take(5)
                .Select(item => new ForecastDay
                {
                    Date = DateTime.Parse(item.DateTimeText),
                    TemperatureC = Math.Round(item.Main.Temp, 1),
                    Description = item.Weather.FirstOrDefault()?.Description ?? "N/A",
                    Icon = item.Weather.FirstOrDefault()?.Icon ?? "01d"
                })
                .ToList();

            var result = new ForecastResponse
            {
                City = data.City.Name,
                Days = days
            };

            _cache.Set(cacheKey, result, TimeSpan.FromMinutes(30));

            return result;
        }
    }
}