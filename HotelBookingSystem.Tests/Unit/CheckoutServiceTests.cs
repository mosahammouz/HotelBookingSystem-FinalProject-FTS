using HotelBookingSystem.Application.DTOs.Checkout;
using HotelBookingSystem.Application.Services;
using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Domain.Enums;
using HotelBookingSystem.Domain.Interfaces;
using Moq;

namespace HotelBookingSystem.Tests.Unit;

public class CheckoutServiceTests
{
    private readonly Mock<ICheckoutRepository> _repositoryMock;
    private readonly CheckoutService _checkoutService;

    public CheckoutServiceTests()
    {
        _repositoryMock = new Mock<ICheckoutRepository>();
        _checkoutService = new CheckoutService(_repositoryMock.Object);
    }

    [Fact]
    public async Task CheckoutAsync_ShouldCreateBooking_WhenRoomIsAvailable()
    {
        // Arrange
        var request = new CheckoutRequest
        {
            HotelId = 1,
            RoomId = 2,
            CheckInDate = new DateTime(2026, 10, 1),
            CheckOutDate = new DateTime(2026, 10, 5),
            SpecialRequests = "Quiet room",
            PaymentMethod = PaymentMethod.CreditCard
        };

        var room = new Room
        {
            Id = 2,
            HotelId = 1,
            RoomNumber = "202",
            PricePerNight = 250
        };

        _repositoryMock.Setup(r => r.GetRoomForCheckoutAsync(1, 2)).ReturnsAsync(room);
        _repositoryMock.Setup(r => r.IsRoomAvailableAsync(2, It.IsAny<DateTime>(), It.IsAny<DateTime>())).ReturnsAsync(true);
        _repositoryMock.Setup(r => r.CreateBookingAsync(It.IsAny<Booking>())).ReturnsAsync((Booking booking) => { booking.Id = 1; return booking; });

        // Act
        var result = await _checkoutService.CheckoutAsync(3, request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.BookingId);
        Assert.Equal(1000, result.TotalPrice);
        Assert.Equal("Pending", result.PaymentStatus);
        Assert.Equal("Pending", result.BookingStatus);

        _repositoryMock.Verify(
            r => r.CreateBookingAsync(It.Is<Booking>(b =>
                b.UserId == 3 &&
                b.TotalPrice == 1000 &&
                b.SpecialRequests == "Quiet room" &&
                b.BookingRooms.Count == 1)),
            Times.Once);
    }

    [Fact]
    public async Task CheckoutAsync_ShouldThrowArgumentException_WhenDatesAreInvalid()
    {
        // Arrange
        var request = new CheckoutRequest
        {
            HotelId = 1,
            RoomId = 2,
            CheckInDate = new DateTime(2026, 10, 5),
            CheckOutDate = new DateTime(2026, 10, 1)
        };

        // Act
        var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            _checkoutService.CheckoutAsync(3, request));

        // Assert
        Assert.Equal(
            "Check-out date must be after check-in date.",
            exception.Message);

        _repositoryMock.Verify(
            r => r.GetRoomForCheckoutAsync(
                It.IsAny<int>(),
                It.IsAny<int>()),
            Times.Never);
    }

    [Fact]
    public async Task CheckoutAsync_ShouldThrowArgumentException_WhenRoomDoesNotExist()
    {
        // Arrange
        var request = new CheckoutRequest
        {
            HotelId = 1,
            RoomId = 999,
            CheckInDate = new DateTime(2026, 10, 1),
            CheckOutDate = new DateTime(2026, 10, 5)
        };

        _repositoryMock
            .Setup(r => r.GetRoomForCheckoutAsync(1, 999))
            .ReturnsAsync((Room?)null);

        // Act
        var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            _checkoutService.CheckoutAsync(3, request));

        // Assert
        Assert.Equal("Room not found.", exception.Message);
    }

    [Fact]
    public async Task CheckoutAsync_ShouldThrowInvalidOperationException_WhenRoomIsUnavailable()
    {
        // Arrange
        var request = new CheckoutRequest
        {
            HotelId = 1,
            RoomId = 2,
            CheckInDate = new DateTime(2026, 10, 1),
            CheckOutDate = new DateTime(2026, 10, 5)
        };

        var room = new Room
        {
            Id = 2,
            HotelId = 1,
            PricePerNight = 250
        };

        _repositoryMock
            .Setup(r => r.GetRoomForCheckoutAsync(1, 2))
            .ReturnsAsync(room);

        _repositoryMock
            .Setup(r => r.IsRoomAvailableAsync(
                2,
                It.IsAny<DateTime>(),
                It.IsAny<DateTime>()))
            .ReturnsAsync(false);

        // Act
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _checkoutService.CheckoutAsync(3, request));

        // Assert
        Assert.Equal(
            "Room is not available for the selected dates.",
            exception.Message);

        _repositoryMock.Verify(
            r => r.CreateBookingAsync(It.IsAny<Booking>()),
            Times.Never);
    }
}