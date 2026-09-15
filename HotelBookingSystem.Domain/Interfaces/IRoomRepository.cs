using HotelBookingSystem.Domain.Entities;

namespace HotelBookingSystem.Domain.Interfaces;

public interface IRoomRepository
{
    Task<IEnumerable<Room>> GetAllAsync();
    Task<Room?> GetByIdAsync(int id);
    Task AddAsync(Room room);
    Task UpdateAsync(Room room);
    Task DeleteAsync(Room room);
}