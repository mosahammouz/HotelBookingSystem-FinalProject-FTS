using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Domain.Enums;
using HotelBookingSystem.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Infrastructure.Persistence.Repositories;

public class CheckoutRepository : ICheckoutRepository
{
    private readonly AppDbContext _dbContext;
    public CheckoutRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Room?> GetRoomForCheckoutAsync(int hotelId, int roomId)
    {
        return await _dbContext.Rooms.FirstOrDefaultAsync(r =>
                r.Id == roomId &&
                r.HotelId == hotelId);
    }

    public async Task<bool> IsRoomAvailableAsync(int roomId, DateTime checkInDate, DateTime checkOutDate)
    {
        return !await _dbContext.BookingRooms.AnyAsync(br =>
                br.RoomId == roomId &&
                (br.Booking.Status == BookingStatus.Pending ||
                 br.Booking.Status == BookingStatus.Confirmed) &&
                br.Booking.CheckInDate < checkOutDate &&
                br.Booking.CheckOutDate > checkInDate);
    }

    public async Task<Booking> CreateBookingAsync(Booking booking)
    {
        _dbContext.Bookings.Add(booking); // add to db
        await _dbContext.SaveChangesAsync();
        return booking;
    }
}