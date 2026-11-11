async function getWeather() {

    const city = document.getElementById("cityInput").value.trim();

    const message = document.getElementById("message");
    const weatherResult = document.getElementById("weatherResult");
    const welcome = document.querySelector(".welcome");

    // Validate city input
    if (city === "") {
        message.textContent = "Please enter a city name.";
        weatherResult.style.display = "none";
        welcome.style.display = "block";
        return;
    }

    // Show loading message
    message.textContent = "Loading weather details...";
    weatherResult.style.display = "none";
    welcome.style.display = "none";

    try {

        // Fetch weather data from ASP.NET Core API
        const response = await fetch(
            `https://localhost:7194/api/Weather/current/${encodeURIComponent(city)}`
        );

        if (!response.ok) {
            throw new Error("Could not retrieve weather data.");
        }

        // Convert response into JSON
        const data = await response.json();

        // Display city and country
        document.getElementById("city").textContent =
            data.city + ", " + data.country;

        // Display weather description
        document.getElementById("description").textContent =
            data.description;

        // Display temperature
        document.getElementById("temperature").textContent =
            data.temperatureC + " °C";

        // Display feels-like temperature
        document.getElementById("feelsLike").textContent =
            data.feelsLikeC + " °C";

        // Display humidity
        document.getElementById("humidity").textContent =
            data.humidity + "%";

        // Display wind speed
        document.getElementById("wind").textContent =
            data.windSpeed + " m/s";

        // Change weather icon based on condition
        const description = data.description.toLowerCase();
        const weatherIcon = document.querySelector(".weather-icon");

        if (description.includes("rain")) {
            weatherIcon.textContent = "🌧️";
        }
        else if (description.includes("cloud")) {
            weatherIcon.textContent = "☁️";
        }
        else if (description.includes("snow")) {
            weatherIcon.textContent = "❄️";
        }
        else if (description.includes("storm")) {
            weatherIcon.textContent = "⛈️";
        }
        else if (description.includes("clear")) {
            weatherIcon.textContent = "☀️";
        }
        else {
            weatherIcon.textContent = "🌤️";
        }

        // Show weather result
        weatherResult.style.display = "block";

        // Clear message
        message.textContent = "";

    }
    catch (error) {

        // Handle API errors
        message.textContent =
            "Unable to load weather. Please check whether your API is running.";

        weatherResult.style.display = "none";
        welcome.style.display = "block";

        console.error("Weather API Error:", error);

    }
}