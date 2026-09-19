using HotelBookingSystem.Domain.Enums;
namespace HotelBookingSystem.Domain.Entities;

public class Room
{
    public int Id { get; set; }
    public int HotelId { get; set; } //FK       
    public string RoomNumber { get; set; } = string.Empty;
    public RoomType RoomType { get; set; }
    public int AdultCapacity { get; set; }
    public int ChildrenCapacity { get; set; }
    public decimal PricePerNight { get; set; } // 18 digits . 2 digits
    public bool IsAvailable { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public Hotel Hotel { get; set; } = null!;
    public ICollection<BookingRoom> BookingRooms { get; set; } = new List<BookingRoom>();
}