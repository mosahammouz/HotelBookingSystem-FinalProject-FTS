using HotelBookingSystem.Application.DTOs.Checkout;
using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Domain.Enums;
using HotelBookingSystem.Domain.Interfaces;

namespace HotelBookingSystem.Application.Services;

public class CheckoutService : ICheckoutService
{
    private readonly ICheckoutRepository _checkoutRepository;
    public CheckoutService(ICheckoutRepository checkoutRepository)
    {
        _checkoutRepository = checkoutRepository;
    }

    public async Task<CheckoutResponse> CheckoutAsync(int userId, CheckoutRequest request)
    {
        if (request.CheckOutDate <= request.CheckInDate)
        {
            throw new ArgumentException("Check-out date must be after check-in date.");
        }

        var room = await _checkoutRepository.GetRoomForCheckoutAsync(request.HotelId, request.RoomId);
        if (room == null) { throw new ArgumentException("Room not found."); }

        var isAvailable = await _checkoutRepository.IsRoomAvailableAsync(request.RoomId, request.CheckInDate, request.CheckOutDate);

        if (!isAvailable)
        { throw new InvalidOperationException("Room is not available for the selected dates."); }
        var nights = (request.CheckOutDate - request.CheckInDate).Days;
        var totalPrice = nights * room.PricePerNight;

        var booking = new Booking
        {
            UserId = userId,
            CheckInDate = request.CheckInDate,
            CheckOutDate = request.CheckOutDate,
            TotalPrice = totalPrice,
            Status = BookingStatus.Pending,
            ConfirmationNumber = $"BK-{Guid.NewGuid():N}",
            SpecialRequests = request.SpecialRequests
        };

        booking.BookingRooms.Add(new BookingRoom
        {
            RoomId = room.Id,
            PricePerNight = room.PricePerNight
        });

        var createdBooking = await _checkoutRepository.CreateBookingAsync(booking);

        return new CheckoutResponse
        {
            BookingId = createdBooking.Id,
            ConfirmationNumber = createdBooking.ConfirmationNumber,
            TotalPrice = createdBooking.TotalPrice,
            PaymentStatus = "Pending",
            BookingStatus = createdBooking.Status.ToString()
        };
    }
}