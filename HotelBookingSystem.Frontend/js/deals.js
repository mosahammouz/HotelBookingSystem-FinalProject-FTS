async function loadDeals() {
    const container = document.getElementById("deals");

    const token = localStorage.getItem("token");

    try {
        const response = await fetch(
            `${API_BASE_URL}/Home/featured-deals`,
            {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            }
        );

        if (!response.ok) {
            container.textContent = "Failed to load deals.";
            return;
        }

        const hotels = await response.json();

        if (hotels.length === 0) {
            container.textContent = "No featured deals available.";
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
                <p>Original: $${hotel.originalPrice}</p>
                <p>Discounted: $${hotel.discountedPrice}</p>
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

loadDeals();