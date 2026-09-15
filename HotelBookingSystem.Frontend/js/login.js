const loginForm = document.getElementById("loginForm");
const errorMessage = document.getElementById("errorMessage");

loginForm.addEventListener("submit", async function (event) {
    event.preventDefault();

    const username = document.getElementById("username").value;
    const password = document.getElementById("password").value;

    try {
        const response = await fetch(`${API_BASE_URL}/Auth/login`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                username: username,
                password: password
            })
        });

        if (!response.ok) {
            errorMessage.textContent = "Invalid username or password.";
            return;
        }

        const data = await response.json();

        localStorage.setItem("token", data.token);

        window.location.href = "index.html";
    }
    catch (error) {
        errorMessage.textContent = "Unable to connect to the server.";
        console.error(error);
    }
});