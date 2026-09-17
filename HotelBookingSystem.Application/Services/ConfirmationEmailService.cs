using HotelBookingSystem.Application.DTOs.Booking_Confirmation;
using HotelBookingSystem.Domain.Interfaces;

namespace HotelBookingSystem.Application.Services;

public class ConfirmationEmailService : IConfirmationEmailService
{
    private readonly IEmailSender _emailSender;
    public ConfirmationEmailService(
        IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    public async Task SendBookingConfirmationEmailAsync(string recipientEmail, BookingConfirmation confirmation)
    {
        var subject = $"Booking Confirmation - {confirmation.ConfirmationNumber}";
        var body = $"""
                    Hello,

                    Your hotel booking has been created successfully.

                    Confirmation Number: {confirmation.ConfirmationNumber}

                    Hotel: {confirmation.HotelName}
                    Address: {confirmation.HotelAddress}

                    Room Number: {confirmation.RoomNumber}
                    Room Type: {confirmation.RoomType}

                    Check-in: {confirmation.CheckInDate:yyyy-MM-dd}
                    Check-out: {confirmation.CheckOutDate:yyyy-MM-dd}

                    Price per night: {confirmation.PricePerNight:C}
                    Total Price: {confirmation.TotalPrice:C}

                    Payment Status: {confirmation.PaymentStatus}
                    Booking Status: {confirmation.BookingStatus}

                    Thank you for booking with us.
                    """;

        await _emailSender.SendEmailAsync(recipientEmail, subject, body);
    }
}