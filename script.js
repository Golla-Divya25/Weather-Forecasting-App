async function getWeather() {
    const cityInput = document.getElementById("cityInput");
    const message = document.getElementById("message");

    const cityName = cityInput.value.trim();

    if (!cityName) {
        message.textContent = "Please enter a city name.";
        return;
    }

    message.textContent = "Loading weather...";

    try {
        // Get city coordinates
        const geoResponse = await fetch(
            `https://geocoding-api.open-meteo.com/v1/search?name=${encodeURIComponent(cityName)}&count=1&language=en&format=json`
        );

        if (!geoResponse.ok) {
            throw new Error("Unable to find city.");
        }

        const geoData = await geoResponse.json();

        if (!geoData.results || geoData.results.length === 0) {
            message.textContent = "City not found. Try another city.";
            return;
        }

        const place = geoData.results[0];

        // Get weather using latitude and longitude
        const weatherResponse = await fetch(
            `https://api.open-meteo.com/v1/forecast?latitude=${place.latitude}&longitude=${place.longitude}&current=temperature_2m,relative_humidity_2m,apparent_temperature,wind_speed_10m,weather_code&timezone=auto`
        );

        if (!weatherResponse.ok) {
            throw new Error("Unable to retrieve weather.");
        }

        const weatherData = await weatherResponse.json();
        const current = weatherData.current;

        document.getElementById("city").textContent =
            `${place.name}, ${place.country}`;

        document.getElementById("description").textContent =
            getWeatherDescription(current.weather_code);

        document.getElementById("temperature").textContent =
            `Temperature: ${current.temperature_2m} °C`;

        document.getElementById("feelsLike").textContent =
            `Feels like: ${current.apparent_temperature} °C`;

        document.getElementById("humidity").textContent =
            `Humidity: ${current.relative_humidity_2m}%`;

        document.getElementById("wind").textContent =
            `Wind speed: ${current.wind_speed_10m} km/h`;

        message.textContent = "";

        document.getElementById("weatherResult").style.display = "block";

    } catch (error) {
        message.textContent =
            "Unable to load weather. Please try again.";

        console.error(error);
    }
}

function getWeatherDescription(code) {
    const descriptions = {
        0: "Clear Sky ☀️",
        1: "Mainly Clear 🌤️",
        2: "Partly Cloudy ⛅",
        3: "Overcast ☁️",
        45: "Foggy 🌫️",
        48: "Depositing Rime Fog 🌫️",
        51: "Light Drizzle 🌦️",
        53: "Moderate Drizzle 🌦️",
        55: "Dense Drizzle 🌧️",
        61: "Slight Rain 🌧️",
        63: "Moderate Rain 🌧️",
        65: "Heavy Rain 🌧️",
        71: "Slight Snow ❄️",
        73: "Moderate Snow ❄️",
        75: "Heavy Snow ❄️",
        80: "Rain Showers 🌧️",
        81: "Moderate Rain Showers 🌧️",
        82: "Heavy Rain Showers 🌧️",
        95: "Thunderstorm ⛈️"
    };

    return descriptions[code] || "Weather information available";
}
