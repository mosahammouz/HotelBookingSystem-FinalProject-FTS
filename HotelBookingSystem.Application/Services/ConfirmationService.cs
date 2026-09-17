using HotelBookingSystem.Application.DTOs.Booking_Confirmation;
using HotelBookingSystem.Domain.Interfaces;

namespace HotelBookingSystem.Application.Services;

public class ConfirmationService : IConfirmationService
{
    private readonly IConfirmationRepository _confirmationRepository;
    private readonly IConfirmationPdfService _confirmationPdfService;
    private readonly IConfirmationEmailService _confirmationEmailService;
    public ConfirmationService(
        IConfirmationRepository confirmationRepository,
        IConfirmationPdfService confirmationPdfService,
        IConfirmationEmailService confirmationEmailService)
    {
        _confirmationRepository = confirmationRepository;
        _confirmationPdfService = confirmationPdfService;
        _confirmationEmailService = confirmationEmailService;
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
    
    public async Task SendConfirmationEmailAsync(
        int userId,
        int bookingId)
    {
        var booking = await _confirmationRepository
            .GetBookingForConfirmationAsync(userId, bookingId);

        if (booking == null)
        {
            throw new KeyNotFoundException(
                "Booking confirmation not found.");
        }

        var confirmation = await GetConfirmationAsync(
            userId,
            bookingId);

        if (confirmation == null)
        {
            throw new KeyNotFoundException(
                "Booking confirmation not found.");
        }

        await _confirmationEmailService
            .SendBookingConfirmationEmailAsync(
                booking.User.Email,
                confirmation);
    }
}