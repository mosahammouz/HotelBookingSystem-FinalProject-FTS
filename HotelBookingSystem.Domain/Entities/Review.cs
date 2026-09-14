namespace HotelBookingSystem.Domain.Entities;

public class Review
{
    public int Id { get; set; }
    public int HotelId { get; set; }
    public int UserId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Hotel Hotel { get; set; } = null!;
    public User User { get; set; } = null!;
}