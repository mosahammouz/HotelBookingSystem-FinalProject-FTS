using HotelBookingSystem.Application.DTOs.Booking_Confirmation;
using HotelBookingSystem.Application.DTOs.Checkout;
using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Domain.Enums;
using HotelBookingSystem.Domain.Interfaces;

namespace HotelBookingSystem.Application.Services;

public class CheckoutService : ICheckoutService
{
    private readonly ICheckoutRepository _checkoutRepository;
    private readonly IConfirmationEmailService _confirmationEmailService;

    public CheckoutService(ICheckoutRepository checkoutRepository , IConfirmationEmailService confirmationEmailService) // to achieve IOC 
    {
        _checkoutRepository = checkoutRepository;
        _confirmationEmailService = confirmationEmailService;

    }

    public async Task<CheckoutResponse> CheckoutAsync(int userId, CheckoutRequest request)
    {request.CheckInDate = DateTime.SpecifyKind(
            request.CheckInDate,
            DateTimeKind.Utc);

        request.CheckOutDate = DateTime.SpecifyKind(
            request.CheckOutDate,
            DateTimeKind.Utc);
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
        var user = await _checkoutRepository.GetUserAsync(userId);

        if (user != null)
        {
            var confirmation = new BookingConfirmation
            {
                ConfirmationNumber =
                    createdBooking.ConfirmationNumber,

                HotelName = room.Hotel.Name,

                HotelAddress = room.Hotel.Location,

                RoomNumber = room.RoomNumber,

                RoomType =  room.RoomType.ToString(),

                CheckInDate =
                    createdBooking.CheckInDate,

                CheckOutDate =
                    createdBooking.CheckOutDate,

                PricePerNight =
                    room.PricePerNight,

                TotalPrice =
                    createdBooking.TotalPrice,

                PaymentStatus = "Pending",

                BookingStatus =
                    createdBooking.Status.ToString()
            };

            await _confirmationEmailService
                .SendBookingConfirmationEmailAsync(
                    user.Email,
                    confirmation);
        }
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