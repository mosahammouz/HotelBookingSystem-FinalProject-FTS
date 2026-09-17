using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Domain.Interfaces;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
namespace HotelBookingSystem.Infrastructure.Third_library_services;

public class ConfirmationPdfGenerator : IConfirmationPdfGenerator
{
    public byte[] GenerateConfirmationPdf(ConfirmationPdfData data)
    {
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(40);
                page.Header().Text("Booking Confirmation").FontSize(24).Bold();
                page.Content().PaddingTop(20).Column(column =>
                    {
                        column.Spacing(10);
                        column.Item().Text($"Confirmation Number: {data.ConfirmationNumber}");
                        column.Item().Text($"Hotel: {data.HotelName}");
                        column.Item().Text($"Address: {data.HotelAddress}");
                        column.Item().Text($"Room Number: {data.RoomNumber}");
                        column.Item().Text($"Room Type: {data.RoomType}");
                        column.Item().Text($"Check-in: {data.CheckInDate:yyyy-MM-dd}");
                        column.Item().Text($"Check-out: {data.CheckOutDate:yyyy-MM-dd}");
                        column.Item().Text($"Price per night: {data.PricePerNight:C}");
                        column.Item().Text($"Total Price: {data.TotalPrice:C}").Bold();
                        column.Item().Text($"Payment Status: {data.PaymentStatus}");
                        column.Item().Text($"Booking Status: {data.BookingStatus}");
                    });

                page.Footer()
                    .AlignCenter()
                    .Text("Thank you for booking with us!");
            });
        });

        return document.GeneratePdf();
    }
}