using HotelBookingSystem.Application.DTOs.HomPage;

namespace HotelBookingSystem.Application.Services;

public interface IHomeService
{
    Task<List<FeaturedDealDto>> GetFeaturedDealsAsync();
    Task<List<RecentlyVisitedHotelDto>> GetRecentlyVisitedAsync(int userId);
    Task<List<TrendingCityDto>> GetTrendingCitiesAsync();
}