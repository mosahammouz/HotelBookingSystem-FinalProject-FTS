using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Infrastructure.Persistence.Repositories;

public class RoomRepository : IRoomRepository
{
    private readonly AppDbContext _dbContext;

    public RoomRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Room>> GetAllAsync()
    {
        return await _dbContext.Rooms
            .Include(r => r.Hotel)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Room?> GetByIdAsync(int id)
    {
        return await _dbContext.Rooms
            .Include(r => r.Hotel)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task AddAsync(Room room)
    {
        await _dbContext.Rooms.AddAsync(room);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Room room)
    {
        _dbContext.Rooms.Update(room);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Room room)
    {
        _dbContext.Rooms.Remove(room);
        await _dbContext.SaveChangesAsync();
    }
}