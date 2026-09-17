using HotelBookingSystem.Domain.Entities;

namespace HotelBookingSystem.Domain.Interfaces;

public interface IConfirmationPdfGenerator
{
    byte[] GenerateConfirmationPdf(ConfirmationPdfData data);
}