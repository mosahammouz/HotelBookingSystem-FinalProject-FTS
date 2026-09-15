using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Infrastructure.Persistence.Repositories;

public class HotelRepository : IHotelRepository
{
    private readonly AppDbContext _dbContext;
    public HotelRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Hotel>> GetAllAsync()
    {
        return await _dbContext.Hotels
            .Include(h => h.City)
            .Include(h => h.Owner)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Hotel?> GetByIdAsync(int id)
    {
        return await _dbContext.Hotels
            .Include(h => h.City)
            .Include(h => h.Owner)
            .FirstOrDefaultAsync(h => h.Id == id);
    }

    public async Task AddAsync(Hotel hotel)
    {
        await _dbContext.Hotels.AddAsync(hotel);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Hotel hotel)
    {
        _dbContext.Hotels.Update(hotel);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Hotel hotel)
    {
        _dbContext.Hotels.Remove(hotel);
        await _dbContext.SaveChangesAsync();
    }
}