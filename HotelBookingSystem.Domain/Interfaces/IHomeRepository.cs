using HotelBookingSystem.Domain.Entities;

namespace HotelBookingSystem.Domain.Interfaces;

public interface IHomeRepository
{
    Task<List<Hotel>> GetFeaturedDealsAsync();
    Task<List<RecentlyVisitedHotel>> GetRecentlyVisitedAsync(int userId);
    Task<List<TrendingCityResult>> GetTrendingCitiesAsync();
}