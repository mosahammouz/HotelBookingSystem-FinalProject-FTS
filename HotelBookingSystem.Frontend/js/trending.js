async function loadTrendingCities() {
    const container = document.getElementById("trendingCities");

    const token = localStorage.getItem("token");

    try {
        const response = await fetch(
            `${API_BASE_URL}/Home/trending-cities`,
            {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            }
        );

        if (!response.ok) {
            container.textContent = "Failed to load trending cities.";
            return;
        }

        const data = await response.json();

        if (!Array.isArray(data)) {
            container.textContent = data.message;
            return;
        }

        if (data.length === 0) {
            container.textContent = "No trending cities found.";
            return;
        }

        data.forEach(city => {
            const div = document.createElement("div");

            div.innerHTML = `
                <img src="${city.thumbnail}"
                     alt="${city.cityName}"
                     width="200">

                <h3>${city.cityName}</h3>
                <p>Visits: ${city.visitCount}</p>
                <hr>
            `;

            container.appendChild(div);
        });
    }
    catch (error) {
        console.error(error);
        container.textContent = "Unable to connect to the server.";
    }
}

loadTrendingCities();