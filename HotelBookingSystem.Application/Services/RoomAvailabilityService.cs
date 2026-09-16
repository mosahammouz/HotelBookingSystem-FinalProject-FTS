using HotelBookingSystem.Application.DTOs.Rooms;
using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Domain.Interfaces;

namespace HotelBookingSystem.Application.Services;

public class RoomAvailabilityService : IRoomAvailabilityService
{
    private readonly IRoomAvailabilityRepository _roomAvailabilityRepository;
    public RoomAvailabilityService(
        IRoomAvailabilityRepository roomAvailabilityRepository)
    {
        _roomAvailabilityRepository = roomAvailabilityRepository;
    }

    public async Task<List<AvailableRoom>> GetAvailableRoomsAsync(int hotelId, DateTime checkInDate, DateTime checkOutDate)
    {
        var criteria = new RoomAvailabilityCriteria
        {
            HotelId = hotelId,
            CheckInDate = checkInDate,
            CheckOutDate = checkOutDate
        };

        var rooms = await _roomAvailabilityRepository.GetAvailableRoomsAsync(criteria);

        return rooms.Select(room => new AvailableRoom {
            RoomId = room.Id,
            RoomNumber = room.RoomNumber,
            RoomType = room.RoomType.ToString(),
            AdultCapacity = room.AdultCapacity,
            ChildrenCapacity = room.ChildrenCapacity,
            PricePerNight = room.PricePerNight
        }).ToList();
    }
}