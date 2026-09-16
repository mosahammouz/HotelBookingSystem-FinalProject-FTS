using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Infrastructure.Persistence.Repositories;

public class HotelSearchRepository: IHotelSearchRepository
{
    private readonly AppDbContext _dbContext;
    public HotelSearchRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Hotel>> SearchAsync(HotelSearchCriteria criteria)
    {
        var query = _dbContext.Hotels
            .Include(h => h.City)
            .Include(h => h.Images)
            .Include(h => h.Rooms)
            .AsQueryable();

        // City filter
        if (criteria.CityId.HasValue)
        {
            query = query.Where(h => h.CityId == criteria.CityId.Value);
        }

        // Star rating filter
        if (criteria.MinStarRating.HasValue)
        {
            query = query.Where(h =>
                h.StarRating >= criteria.MinStarRating.Value);
        }

        // Minimum price
        if (criteria.MinPrice.HasValue)
        {
            query = query.Where(h =>
                h.Rooms.Any(r =>
                    r.PricePerNight >= criteria.MinPrice.Value));
        }

        // Maximum price
        if (criteria.MaxPrice.HasValue)
        {
            query = query.Where(h =>
                h.Rooms.Any(r =>
                    r.PricePerNight <= criteria.MaxPrice.Value));
        }

        // Room type
        if (criteria.RoomType.HasValue)
        {
            query = query.Where(h =>
                h.Rooms.Any(r =>
                    r.RoomType == criteria.RoomType.Value));
        }

        // Pagination
        return await query
            .Skip((criteria.Page - 1) * criteria.PageSize)
            .Take(criteria.PageSize)
            .ToListAsync();
    }
}