using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Infrastructure.Persistence.Repositories;

public class CityRepository : ICityRepository
{
    private readonly AppDbContext _dbContext;
    public CityRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<City>> GetAllAsync()
    {
        // AsNoTracking improves performance for read-only operations
        return await _dbContext.Cities.AsNoTracking().ToListAsync();
    }

    public async Task<City?> GetByIdAsync(int id)
    {
        return await _dbContext.Cities.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task AddAsync(City city)
    {
        await _dbContext.Cities.AddAsync(city);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(City city)
    {
        _dbContext.Cities.Update(city);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(City city)
    {
        _dbContext.Cities.Remove(city);
        await _dbContext.SaveChangesAsync();
    }
}