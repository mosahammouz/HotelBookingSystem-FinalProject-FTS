namespace HotelBookingSystem.Domain.Entities;

public class City
{
    public int Id { get; set; } // for 1NF
    public string Name { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string PostOffice { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();
}