namespace HotelBookingSystem.Domain.Interfaces;

public interface IEmailSender
{
    Task SendEmailAsync(string recipientEmail, string subject, string body);
}