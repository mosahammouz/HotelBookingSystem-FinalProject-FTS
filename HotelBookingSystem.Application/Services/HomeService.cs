using HotelBookingSystem.Application.DTOs.HomPage;
using HotelBookingSystem.Domain.Interfaces;

namespace HotelBookingSystem.Application.Services;

public class HomeService : IHomeService
{
   private readonly IHomeRepository _homeRepository;
    public HomeService(IHomeRepository homeRepository)
    {
        _homeRepository = homeRepository;
    }

    public async Task<List<FeaturedDealDto>> GetFeaturedDealsAsync()
    {
        var hotels = await _homeRepository.GetFeaturedDealsAsync();
        return hotels.Select(h =>
        {
            var originalPrice = h.Rooms.Any() ? h.Rooms.Min(r => r.PricePerNight) : 0;

            return new FeaturedDealDto
            {
                Id = h.Id,
                Name = h.Name,
                City = h.City.Name,
                StarRating = h.StarRating,
                Thumbnail = h.Images.Select(i => i.ImageUrl).FirstOrDefault(),
                OriginalPrice = originalPrice,

                // Temporary 20% discount
                DiscountedPrice = originalPrice * 0.80m
            };
        }).ToList();
    }

    public async Task<List<RecentlyVisitedHotelDto>> GetRecentlyVisitedAsync(
        int userId)
    {
        var visitedHotels = await _homeRepository.GetRecentlyVisitedAsync(userId);
        return visitedHotels.Select(r => new RecentlyVisitedHotelDto
        {
            Id = r.Hotel.Id,
            Name = r.Hotel.Name,
            City = r.Hotel.City.Name,
            StarRating = r.Hotel.StarRating,
            Thumbnail = r.Hotel.Images.Select(i => i.ImageUrl).FirstOrDefault(),
            Price = r.Hotel.Rooms.Any() ? r.Hotel.Rooms.Min(r => r.PricePerNight) : 0

        }).ToList();
    }

    public async Task<List<TrendingCityDto>> GetTrendingCitiesAsync()
    {
        var cities = await _homeRepository.GetTrendingCitiesAsync();

        return cities.Select(result => new TrendingCityDto
        {
            CityId = result.City.Id,
            CityName = result.City.Name,

            Thumbnail = result.City.Hotels
                .OrderByDescending(h => h.StarRating) // top most rated hotel thumbnail
                .SelectMany(h => h.Images)
                .Select(i => i.ImageUrl)
                .FirstOrDefault(),

            VisitCount = result.VisitCount

        }).ToList();
    }
}