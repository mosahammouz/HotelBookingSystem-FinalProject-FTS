using HotelBookingSystem.Domain.Entities;
namespace HotelBookingSystem.Domain.Interfaces;

public interface ICityRepository
{
    Task<IEnumerable<City>> GetAllAsync();
    Task<City?> GetByIdAsync(int id);
    Task AddAsync(City city);
    Task UpdateAsync(City city);
    Task DeleteAsync(City city);
}