using HotelBookingSystem.Domain.Enums;
namespace HotelBookingSystem.Application.DTOs.Checkout;

public class CheckoutRequest
{
    public int HotelId { get; set; }
    public int RoomId { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public string? SpecialRequests { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
}