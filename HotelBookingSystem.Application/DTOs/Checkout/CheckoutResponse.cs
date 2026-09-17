namespace HotelBookingSystem.Application.DTOs.Checkout;

public class CheckoutResponse
{
    public int BookingId { get; set; }
    public string ConfirmationNumber { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }
    public string PaymentStatus { get; set; } = string.Empty;
    public string BookingStatus { get; set; } = string.Empty;
}