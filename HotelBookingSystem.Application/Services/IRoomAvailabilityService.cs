using HotelBookingSystem.Application.DTOs.Rooms;

namespace HotelBookingSystem.Application.Services;

public interface IRoomAvailabilityService
{
    Task<List<AvailableRoom>> GetAvailableRoomsAsync(int hotelId, DateTime checkInDate, DateTime checkOutDate);
}