namespace HotelBookingSystem.Application.DTOs.Cities;

public class CityResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string PostOffice { get; set; } = string.Empty;
}