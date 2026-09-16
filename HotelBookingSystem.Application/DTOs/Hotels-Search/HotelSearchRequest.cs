using HotelBookingSystem.Domain.Enums;

namespace HotelBookingSystem.Application.DTOs.Hotels_Search;

public class HotelSearchRequest
{
    public int? CityId { get; set; }
    public decimal? MaxPrice { get; set; }
    public decimal? MinPrice { get; set; }
    public int? MinStarRating { get; set; }
    public List<int>? AmenityIds { get; set; }
    public RoomType? RoomType { get; set; }

    public int Page { get; set; } = 1;
    public int PagSize { get; set; } = 10;

}