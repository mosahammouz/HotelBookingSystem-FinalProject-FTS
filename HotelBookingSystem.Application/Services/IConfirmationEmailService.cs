using HotelBookingSystem.Application.DTOs.Booking_Confirmation;

namespace HotelBookingSystem.Application.Services;

public interface IConfirmationEmailService
{
    Task SendBookingConfirmationEmailAsync(string recipientEmail, BookingConfirmation confirmation);
}