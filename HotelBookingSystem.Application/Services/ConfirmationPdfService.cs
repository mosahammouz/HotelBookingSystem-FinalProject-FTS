using HotelBookingSystem.Application.DTOs.Booking_Confirmation;
using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Domain.Interfaces;

namespace HotelBookingSystem.Application.Services;


public class ConfirmationPdfService : IConfirmationPdfService
{
    private readonly IConfirmationPdfGenerator _pdfGenerator;
    public ConfirmationPdfService(
        IConfirmationPdfGenerator pdfGenerator)
    {
        _pdfGenerator = pdfGenerator;
    }

    public byte[] GenerateConfirmationPdf(BookingConfirmation confirmation)
    {
        var pdfData = new ConfirmationPdfData
        {
            ConfirmationNumber = confirmation.ConfirmationNumber,
            HotelName = confirmation.HotelName,
            HotelAddress = confirmation.HotelAddress,
            RoomNumber = confirmation.RoomNumber,
            RoomType = confirmation.RoomType,
            CheckInDate = confirmation.CheckInDate,
            CheckOutDate = confirmation.CheckOutDate,
            PricePerNight = confirmation.PricePerNight,
            TotalPrice = confirmation.TotalPrice,
            PaymentStatus = confirmation.PaymentStatus,
            BookingStatus = confirmation.BookingStatus
        };

        return _pdfGenerator.GenerateConfirmationPdf(pdfData);
    }
}