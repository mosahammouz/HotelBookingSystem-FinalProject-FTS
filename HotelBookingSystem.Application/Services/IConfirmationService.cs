using HotelBookingSystem.Application.DTOs.Booking_Confirmation;

namespace HotelBookingSystem.Application.Services;

public interface IConfirmationService
{
    Task<BookingConfirmation?> GetConfirmationAsync(
        int userId,
        int bookingId);

    Task<byte[]?> GenerateConfirmationPdfAsync(
        int userId,
        int bookingId);
    
    Task SendConfirmationEmailAsync(
        int userId,
        int bookingId);
}