using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Domain.Enums;
using HotelBookingSystem.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Infrastructure.Persistence.Repositories;

public class RoomAvailabilityRepository : IRoomAvailabilityRepository
{
    private readonly AppDbContext _dbContext;
    public RoomAvailabilityRepository(AppDbContext appDbContext)
    {
        _dbContext = appDbContext;
    }

    public async Task<List<Room>> GetAvailableRoomsAsync(RoomAvailabilityCriteria criteria)
    {
        return await _dbContext.Rooms
            .Include(r => r.Hotel)
            .Where(r => r.HotelId == criteria.HotelId)
            .Where(r => r.IsAvailable)
            .Where(r => !r.BookingRooms.Any(br =>
                (br.Booking.Status == BookingStatus.Pending ||
                 br.Booking.Status == BookingStatus.Confirmed)
                &&
                br.Booking.CheckInDate < criteria.CheckOutDate
                &&
                br.Booking.CheckOutDate > criteria.CheckInDate
            ))
            .ToListAsync();
    }
}