namespace HotelBookingSystem.Application.DTOs.Hotels;

public class UpdateHotelRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int StarRating { get; set; }
    public int CityId { get; set; }
    public int OwnerId { get; set; }
    public string Location { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}