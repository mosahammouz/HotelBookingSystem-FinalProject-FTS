using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Infrastructure.Persistence.Repositories;
public class HomeRepository : IHomeRepository
{
    private readonly AppDbContext _dbContext;
    public HomeRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Hotel>> GetFeaturedDealsAsync() // 5 hotels
    {
        return await _dbContext.Hotels
            .Include(h => h.City)
            .Include(h => h.Images)
            .Include(h => h.Rooms)
            .Where(h => h.Rooms.Any())
            .Take(5)
            .ToListAsync();
    }

    public async Task<List<RecentlyVisitedHotel>> GetRecentlyVisitedAsync(
        int userId)
    {
        return await _dbContext.RecentlyVisitedHotels
            .Include(r => r.Hotel)
            .ThenInclude(h => h.City)
            .Include(r => r.Hotel)
            .ThenInclude(h => h.Images)
            .Include(r => r.Hotel)
            .ThenInclude(h => h.Rooms)
            .Where(r => r.UserId == userId)
            .OrderByDescending(r => r.VisitedAt)
            .Take(5)
            .ToListAsync();
    }

    public async Task<List<TrendingCityResult>> GetTrendingCitiesAsync() // return the most 5 
    {
        var results = await _dbContext.RecentlyVisitedHotels
            .GroupBy(r => new // anonymous object
            {
                r.Hotel.CityId,
                CityName = r.Hotel.City.Name
            })
            .OrderByDescending(g => g.Count()) // g is one group
            .Take(5)
            .Select(g => new
            {
                CityId = g.Key.CityId,
                VisitCount = g.Count()
            })
            .ToListAsync();

        var cityIds = results.Select(x => x.CityId).ToList();

        var cities = await _dbContext.Cities
            .Include(c => c.Hotels)
            .ThenInclude(h => h.Images)
            .Where(c => cityIds.Contains(c.Id))
            .ToListAsync();

        return results.Select(result => new TrendingCityResult
        {
            City = cities.First(c => c.Id == result.CityId),
            VisitCount = result.VisitCount
        }).ToList();
    }
}