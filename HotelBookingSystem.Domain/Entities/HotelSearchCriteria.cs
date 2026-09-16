using HotelBookingSystem.Domain.Enums;

namespace HotelBookingSystem.Domain.Entities;

public class HotelSearchCriteria
{
    public int? CityId { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public int? MinStarRating { get; set; }
    public List<int>? AmenityIds { get; set; }
    public RoomType? RoomType { get; set; }

    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}