namespace HotelBookingSystem.Application.DTOs.HomPage;

public class FeaturedDealDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public int StarRating { get; set; }
    public string? Thumbnail { get; set; }
    public decimal OriginalPrice { get; set; }
    public decimal DiscountedPrice { get; set; }
}

public class RecentlyVisitedHotelDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public int StarRating { get; set; }
    public string? Thumbnail { get; set; }
    public decimal Price { get; set; }
}

public class TrendingCityDto
{
    public int CityId { get; set; }
    public string CityName { get; set; } = string.Empty;
    public int VisitCount { get; set; }
    public string? Thumbnail { get; set; }
}