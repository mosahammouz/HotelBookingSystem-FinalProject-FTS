using HotelBookingSystem.Domain.Enums;
namespace HotelBookingSystem.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public UserRole Role { get; set; } = UserRole.Customer; //  Admin or Regular user (Customer)
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<RecentlyVisitedHotel> RecentlyVisitedHotels { get; set; } = new List<RecentlyVisitedHotel>();
}