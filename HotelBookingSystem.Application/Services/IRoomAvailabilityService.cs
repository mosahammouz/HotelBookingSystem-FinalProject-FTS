using HotelBookingSystem.Application.DTOs.Rooms;

namespace HotelBookingSystem.Application.Services;

public interface IRoomAvailabilityService
{
    Task<List<AvailableRoom>> GetAvailableRoomsAsync(RoomAvailabilityRequest request);
}