using System.Net;
using System.Net.Http.Json;
using HotelBookingSystem.Application.DTOs.Checkout;

namespace HotelBookingSystem.Tests.Integration;

public class CheckoutIntegrationTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    public CheckoutIntegrationTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Checkout_ShouldReturnUnauthorized_WhenUserIsNotAuthenticated()
    {
        // Arrange
        var request = new CheckoutRequest
        {
            HotelId = 1,
            RoomId = 2,
            CheckInDate = new DateTime(2026, 10, 1),
            CheckOutDate = new DateTime(2026, 10, 5),
            PaymentMethod = Domain.Enums.PaymentMethod.CreditCard
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/bookings/checkout", request);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}