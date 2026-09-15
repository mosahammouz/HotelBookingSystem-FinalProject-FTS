namespace HotelBookingSystem.Application.DTOs.Cities;

public class UpdateCityRequest
{
    public string Name { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string PostOffice { get; set; } = string.Empty;
}