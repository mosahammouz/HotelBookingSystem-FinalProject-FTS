namespace HotelBookingSystem.Domain.Entities;

public class ConfirmationPdfData
{
    public string ConfirmationNumber { get; set; } = string.Empty;

    public string HotelName { get; set; } = string.Empty;
    public string HotelAddress { get; set; } = string.Empty;

    public string RoomNumber { get; set; } = string.Empty;
    public string RoomType { get; set; } = string.Empty;

    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }

    public decimal PricePerNight { get; set; }
    public decimal TotalPrice { get; set; }

    public string PaymentStatus { get; set; } = string.Empty;
    public string BookingStatus { get; set; } = string.Empty;
}