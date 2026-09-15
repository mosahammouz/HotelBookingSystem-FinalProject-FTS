using HotelBookingSystem.Domain.Enums;

namespace HotelBookingSystem.Application.DTOs.Rooms;

public class UpdateRoomRequest
{
    public int HotelId { get; set; }
    public string RoomNumber { get; set; } = string.Empty;
    public RoomType RoomType { get; set; }
    public int AdultCapacity { get; set; }
    public int ChildrenCapacity { get; set; }
    public decimal PricePerNight { get; set; }
}