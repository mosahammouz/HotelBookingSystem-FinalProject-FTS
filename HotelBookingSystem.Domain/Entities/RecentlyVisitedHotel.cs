namespace HotelBookingSystem.Domain.Entities;

public class RecentlyVisitedHotel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int HotelId { get; set; }
    public DateTime VisitedAt { get; set; } = DateTime.UtcNow;
    public User User { get; set; } = null!;
    public Hotel Hotel { get; set; } = null!;
}