using HotelBookingSystem.Application.DTOs.Booking_Confirmation;
using HotelBookingSystem.Domain.Interfaces;

namespace HotelBookingSystem.Application.Services;

public class ConfirmationService : IConfirmationService
{
    private readonly IConfirmationRepository _confirmationRepository;
    private readonly IConfirmationPdfService _confirmationPdfService;

    public ConfirmationService(
        IConfirmationRepository confirmationRepository,
        IConfirmationPdfService confirmationPdfService)
    {
        _confirmationRepository = confirmationRepository;
        _confirmationPdfService = confirmationPdfService;
    }

    public async Task<BookingConfirmation?> GetConfirmationAsync(
        int userId,
        int bookingId)
    {
        var booking = await _confirmationRepository
            .GetBookingForConfirmationAsync(userId, bookingId);

        if (booking == null)
        {
            return null;
        }

        var bookingRoom = booking.BookingRooms.FirstOrDefault();

        if (bookingRoom == null)
        {
            return null;
        }

        var room = bookingRoom.Room;
        var hotel = room.Hotel;

        return new BookingConfirmation
        {
            ConfirmationNumber = booking.ConfirmationNumber,

            HotelName = hotel.Name,
            HotelAddress = hotel.Location,

            RoomId = room.Id,
            RoomNumber = room.RoomNumber,
            RoomType = room.RoomType.ToString(),

            CheckInDate = booking.CheckInDate,
            CheckOutDate = booking.CheckOutDate,

            PricePerNight = bookingRoom.PricePerNight,
            TotalPrice = booking.TotalPrice,

            PaymentStatus = booking.Payment?.Status.ToString() ?? "Pending",
            BookingStatus = booking.Status.ToString()
        };
    }

    public async Task<byte[]?> GenerateConfirmationPdfAsync(
        int userId,
        int bookingId)
    {
        var confirmation = await GetConfirmationAsync(
            userId,
            bookingId);

        if (confirmation == null)
        {
            return null;
        }

        return _confirmationPdfService
            .GenerateConfirmationPdf(confirmation);
    }
}