async function loadRecentlyVisited() {
    const container = document.getElementById("recentlyVisited");

    const token = localStorage.getItem("token");

    try {
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

        hotels.forEach(hotel => {
            const div = document.createElement("div");

            div.innerHTML = `
                <img src="${hotel.thumbnail}"
                     alt="${hotel.name}"
                     width="200">

                <h3>${hotel.name}</h3>
                <p>City: ${hotel.city}</p>
                <p>⭐ ${hotel.starRating}</p>
                <p>Price: $${hotel.price} / night</p>
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

loadRecentlyVisited();