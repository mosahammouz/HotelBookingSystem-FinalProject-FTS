using HotelBookingSystem.Application.DTOs.Booking_Confirmation;

namespace HotelBookingSystem.Application.Services;

public interface IConfirmationPdfService
{
    byte[] GenerateConfirmationPdf(BookingConfirmation confirmation);

}