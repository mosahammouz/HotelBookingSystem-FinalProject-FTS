using HotelBookingSystem.Domain.Entities;

namespace HotelBookingSystem.Domain.Interfaces;

public interface IRoomAvailabilityRepository
{
    Task<List<Room>> GetAvailableRoomsAsync(RoomAvailabilityCriteria criteria);
}