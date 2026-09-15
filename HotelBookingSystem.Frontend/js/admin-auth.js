const token = localStorage.getItem("token");

if (!token) {
    window.location.href = "login.html";
} else {
    const payload = JSON.parse(atob(token.split(".")[1]));

    if (payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] !== "Admin") {
        window.location.href = "home.html";
    }
}