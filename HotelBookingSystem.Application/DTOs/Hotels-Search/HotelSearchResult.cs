namespace HotelBookingSystem.Application.DTOs.Hotels_Search;

public class HotelSearchResult
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int StarRating { get; set; }
    public decimal PricePerNight { get; set; }
    public string Description { get; set; } = string.Empty;
    public string? Thumbnail { get; set; }
}