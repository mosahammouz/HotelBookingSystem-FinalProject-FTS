using HotelBookingSystem.Application.DTOs.Rooms;

namespace HotelBookingSystem.Application.Services;

public interface IRoomService
{
    Task<IEnumerable<RoomResponse>> GetAllAsync();
    Task<RoomResponse?> GetByIdAsync(int id);
    Task<RoomResponse> CreateAsync(CreateRoomRequest request);
    Task<RoomResponse?> UpdateAsync(int id, UpdateRoomRequest request);
    Task<bool> DeleteAsync(int id);
}