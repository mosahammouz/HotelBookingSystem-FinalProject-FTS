using HotelBookingSystem.Domain.Enums;

namespace HotelBookingSystem.Application.DTOs.Rooms;

public class RoomResponse
{
    public int Id { get; set; }
    public int HotelId { get; set; }
    public string HotelName { get; set; } = string.Empty;
    public string RoomNumber { get; set; } = string.Empty;
    public RoomType RoomType { get; set; }
    public int AdultCapacity { get; set; }
    public int ChildrenCapacity { get; set; }
    public decimal PricePerNight { get; set; }
}