using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using HotelBookingSystem.Application.DTOs.Checkout;
using HotelBookingSystem.Application.Services;
using HotelBookingSystem.Domain.Enums;
using HotelBookingSystem.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace HotelBookingSystem.Tests.Integration;

public class CheckoutAuthenticatedIntegrationTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public CheckoutAuthenticatedIntegrationTests(
        CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Checkout_ShouldCreateBooking_WhenUserIsAuthenticated()
    {
        // Arrange
        await _factory.SeedTestDataAsync();

        using var scope = _factory.Services.CreateScope();

        var jwtService = scope.ServiceProvider
            .GetRequiredService<IJwtTokenService>();

        var dbContext = scope.ServiceProvider
            .GetRequiredService<AppDbContext>();

        var user = dbContext.Users
            .OrderByDescending(u => u.Id)
            .First();

        var hotel = dbContext.Hotels
            .OrderByDescending(h => h.Id)
            .First();

        var room = dbContext.Rooms
            .OrderByDescending(r => r.Id)
            .First();

        var token = jwtService.GenerateToken(user);

        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var request = new CheckoutRequest
        {
            HotelId = hotel.Id,
            RoomId = room.Id,
            CheckInDate = new DateTime(2026, 10, 1),
            CheckOutDate = new DateTime(2026, 10, 5),
            SpecialRequests = "Quiet room",
            PaymentMethod = PaymentMethod.CreditCard
        };

        // Act
        var response = await _client.PostAsJsonAsync(
            "/api/bookings/checkout",
            request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}