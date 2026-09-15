namespace HotelBookingSystem.Application.DTOs.Hotels;

public class HotelResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int StarRating { get; set; }
    public int CityId { get; set; }
    public string CityName { get; set; } = string.Empty;
    public int OwnerId { get; set; }
    public string OwnerUsername { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}