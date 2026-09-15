using HotelBookingSystem.Domain.Entities;
namespace HotelBookingSystem.Domain.Interfaces;

public interface IHotelRepository
{
    Task<IEnumerable<Hotel>> GetAllAsync();
    Task<Hotel?> GetByIdAsync(int id);
    Task AddAsync(Hotel hotel);
    Task UpdateAsync(Hotel hotel);
    Task DeleteAsync(Hotel hotel);
}