using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Infrastructure.Persistence.Repositories;

public class ConfirmationRepository : IConfirmationRepository
{
    private readonly AppDbContext _dbContext;
    public ConfirmationRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Booking?> GetBookingForConfirmationAsync(int userId, int bookingId)
    {
        return await _dbContext.Bookings
            .Include(b => b.BookingRooms)
            .ThenInclude(br => br.Room)
            .ThenInclude(r => r.Hotel)
            .Include(b => b.Payment)
            .FirstOrDefaultAsync(b =>
                b.Id == bookingId &&
                b.UserId == userId);
    }
}