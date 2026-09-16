async function loadFeaturedDeals() {
    const container = document.getElementById("featuredDeals");

    ```
try {
    const token = localStorage.getItem("token");

    const response = await fetch(
        `${API_BASE_URL}/Home/featured-deals`,
        {
            headers: {
                "Authorization": `Bearer ${token}`
            }
        }
    );

    if (!response.ok) {
        container.textContent = "Failed to load featured deals.";
        return;
    }

    const hotels = await response.json();

    if (hotels.length === 0) {
        container.textContent = "No featured deals available.";
        return;
    }

    container.innerHTML = "";

    hotels.forEach(hotel => {
        const card = document.createElement("div");
        card.className = "hotel-card";

        card.innerHTML = `
    <img src="${hotel.thumbnail}" alt="${hotel.name}">

        <h3>${hotel.name}</h3>

    <p>${hotel.city}</p>

    <p>⭐ ${hotel.starRating}</p>

    <p>
        <del>$${hotel.originalPrice}</del>
        <strong>$${hotel.discountedPrice}</strong>
        / night
    </p>
        `;

        container.appendChild(card);
    });
}
catch (error) {
    console.error(error);
    container.textContent = "Unable to connect to the server.";
}
```

}

async function loadRecentlyVisited() {
    const container = document.getElementById("recentlyVisited");

    ```
try {
    const token = localStorage.getItem("token");

    const response = await fetch(
        `${API_BASE_URL}/Home/recently-visited`,
        {
            headers: {
                "Authorization": `Bearer ${token}`
            }
        }
    );

    if (!response.ok) {
        container.textContent = "Failed to load recently visited hotels.";
        return;
    }

    const hotels = await response.json();

    if (hotels.length === 0) {
        container.textContent = "No recently visited hotels.";
        return;
    }

    container.innerHTML = "";

    hotels.forEach(hotel => {
        const card = document.createElement("div");
        card.className = "hotel-card";

        card.innerHTML = `
    <img src="${hotel.thumbnail}" alt="${hotel.name}">

        <h3>${hotel.name}</h3>

    <p>${hotel.city}</p>

    <p>⭐ ${hotel.starRating}</p>

    <p>$${hotel.price} / night</p>
        `;

        container.appendChild(card);
    });
}
catch (error) {
    console.error(error);
    container.textContent = "Unable to connect to the server.";
}
```

}

async function loadTrendingCities() {
    const container = document.getElementById("trendingCities");

    ```
try {
    const token = localStorage.getItem("token");

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

    container.innerHTML = "";

    data.forEach(city => {
        const card = document.createElement("div");
        card.className = "hotel-card";

        card.innerHTML = `
    <img src="${city.thumbnail}" alt="${city.cityName}">

        <h3>${city.cityName}</h3>

    <p>Visits: ${city.visitCount}</p>
        `;

        container.appendChild(card);
    });
}
catch (error) {
    console.error(error);
    container.textContent = "Unable to connect to the server.";
}
```

}

loadFeaturedDeals();
loadRecentlyVisited();
loadTrendingCities();
