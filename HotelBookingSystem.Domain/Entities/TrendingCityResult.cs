namespace HotelBookingSystem.Domain.Entities;

public class TrendingCityResult
{
    public City City { get; set; } = null!;
    public int VisitCount { get; set; }
}