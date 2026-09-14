namespace HotelBookingSystem.Domain.Entities;

public class Amenity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<HotelAmenity> HotelAmenities { get; set; } = new List<HotelAmenity>();
}