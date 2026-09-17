using System.Net;
using System.Net.Mail;
using HotelBookingSystem.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace HotelBookingSystem.Infrastructure.Email;

public class EmailSender : IEmailSender

{
    private readonly IConfiguration _configuration;
    public EmailSender(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string recipientEmail, string subject, string body)
    {
        var smtpHost = _configuration["Email:SmtpHost"];
        var smtpPort = int.Parse(_configuration["Email:SmtpPort"]!);

        var username = _configuration["Email:Username"];
        var password = _configuration["Email:Password"];
        var fromEmail = _configuration["Email:FromEmail"];

        using var client = new SmtpClient(smtpHost, smtpPort)
        {
            EnableSsl = true, Credentials = new NetworkCredential(username, password)
        };

        using var message = new MailMessage(fromEmail!, recipientEmail, subject, body);
        message.IsBodyHtml = false;
        await client.SendMailAsync(message);
    }
}