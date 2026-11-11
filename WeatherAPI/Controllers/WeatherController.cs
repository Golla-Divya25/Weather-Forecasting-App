
using Microsoft.AspNetCore.Mvc;
using WeatherAPI.Services;

namespace WeatherAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherServices _weatherService;

        public WeatherController(IWeatherServices weatherService)
        {
            _weatherService = weatherService;
        }

        // GET: api/weather/current/Hyderabad
        [HttpGet("current/{city}")]
        public async Task<IActionResult> GetCurrent(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                return BadRequest(new
                {
                    message = "City name cannot be empty."
                });
            }

            var result =
                await _weatherService.GetCurrentWeatherAsync(city);

            if (result == null)
            {
                return NotFound(new
                {
                    message = $"Weather data not found for {city}."
                });
            }

            return Ok(result);
        }

        // GET: api/weather/forecast/Hyderabad
        [HttpGet("forecast/{city}")]
        public async Task<IActionResult> GetForecast(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                return BadRequest(new
                {
                    message = "City name cannot be empty."
                });
            }

            var result =
                await _weatherService.GetForecastAsync(city);

            if (result == null)
            {
                return NotFound(new
                {
                    message = $"Forecast not found for {city}."
                });
            }

            return Ok(result);
        }
    }
}
