namespace HotelBookingSystem.Application.DTOs.Rooms;

public class AvailableRoom
{
    public int RoomId { get; set; }
    public string RoomNumber { get; set; } = string.Empty;
    public string RoomType { get; set; } = string.Empty;
    public int AdultCapacity { get; set; }
    public int ChildrenCapacity { get; set; }
    public decimal PricePerNight { get; set; }
}