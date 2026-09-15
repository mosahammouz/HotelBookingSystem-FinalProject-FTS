using HotelBookingSystem.Application.DTOs.Rooms;
using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Domain.Interfaces;

namespace HotelBookingSystem.Application.Services;

public class RoomService : IRoomService
{
    private readonly IRoomRepository _roomRepository;
    public RoomService(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task<IEnumerable<RoomResponse>> GetAllAsync()
    {
        var rooms = await _roomRepository.GetAllAsync();
        return rooms.Select(MapToResponse);
    }

    public async Task<RoomResponse?> GetByIdAsync(int id)
    {
        var room = await _roomRepository.GetByIdAsync(id);

        if (room == null) { return null; }

        return MapToResponse(room);
    }

    public async Task<RoomResponse> CreateAsync(
        CreateRoomRequest request)
    {
        var room = new Room
        {
            HotelId = request.HotelId,
            RoomNumber = request.RoomNumber,
            RoomType = request.RoomType,
            AdultCapacity = request.AdultCapacity,
            ChildrenCapacity = request.ChildrenCapacity,
            PricePerNight = request.PricePerNight
        };

        await _roomRepository.AddAsync(room);
        var createdRoom = await _roomRepository.GetByIdAsync(room.Id);
        return MapToResponse(createdRoom!);
    }

    public async Task<RoomResponse?> UpdateAsync(
        int id,
        UpdateRoomRequest request)
    {
        var room = await _roomRepository.GetByIdAsync(id);
        if (room == null) { return null; }

        room.HotelId = request.HotelId;
        room.RoomNumber = request.RoomNumber;
        room.RoomType = request.RoomType;
        room.AdultCapacity = request.AdultCapacity;
        room.ChildrenCapacity = request.ChildrenCapacity;
        room.PricePerNight = request.PricePerNight;
        room.UpdatedAt = DateTime.UtcNow;

        await _roomRepository.UpdateAsync(room);
        return MapToResponse(room);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var room = await _roomRepository.GetByIdAsync(id);

        if (room == null) { return false; }
        await _roomRepository.DeleteAsync(room);
        return true;
    }

    private static RoomResponse MapToResponse(Room room)
    {
        return new RoomResponse
        {
            Id = room.Id,
            HotelId = room.HotelId,
            HotelName = room.Hotel.Name,
            RoomNumber = room.RoomNumber,
            RoomType = room.RoomType,
            AdultCapacity = room.AdultCapacity,
            ChildrenCapacity = room.ChildrenCapacity,
            PricePerNight = room.PricePerNight
        };
    }
}