using HotelBookingSystem.Application.DTOs.Hotels_Search;
using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Domain.Interfaces;

namespace HotelBookingSystem.Application.Services;

public class HotelSearchService :IHotelSearchService
{
    private readonly IHotelSearchRepository _hotelSearchRepository;

    public HotelSearchService(IHotelSearchRepository hotelSearchRepository)
    {
        _hotelSearchRepository = hotelSearchRepository;
    }

    public async Task<List<HotelSearchResult>> SearchAsync(
        HotelSearchRequest request)
    {
        var criteria = new HotelSearchCriteria
        {
            CityId = request.CityId,
            MinPrice = request.MinPrice,
            MaxPrice = request.MaxPrice,
            MinStarRating = request.MinStarRating,
            AmenityIds = request.AmenityIds,
            RoomType = request.RoomType,
            Page = request.Page,
            PageSize = request.PageSize
        };

        var hotels = await _hotelSearchRepository.SearchAsync(criteria);

        return hotels.Select(h => new HotelSearchResult
        {
            Id = h.Id,
            Name = h.Name,
            StarRating = h.StarRating,
            Description = h.Description,
            Thumbnail = h.Images
                .Select(i => i.ImageUrl)
                .FirstOrDefault(),
            PricePerNight = h.Rooms.Any()
                ? h.Rooms.Min(r => r.PricePerNight)
                : 0
        }).ToList();
    }
}