using HotelBookingSystem.Domain.Entities;

namespace HotelBookingSystem.Domain.Interfaces;

public interface IHotelSearchRepository
{
    Task<List<Hotel>> SearchAsync(HotelSearchCriteria criteria);

}