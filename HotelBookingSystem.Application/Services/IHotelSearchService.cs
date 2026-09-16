using HotelBookingSystem.Application.DTOs.Hotels_Search;

namespace HotelBookingSystem.Application.Services;

public interface IHotelSearchService
{
    Task<List<HotelSearchResult>> SearchAsync(HotelSearchRequest request);

}