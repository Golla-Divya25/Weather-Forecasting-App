
using WeatherAPI.Models;

namespace WeatherAPI.Services
{
    public interface IWeatherServices
    {
        Task<WeatherResponse?> GetCurrentWeatherAsync(string city);

        Task<ForecastResponse?> GetForecastAsync(string city);
    }
}
