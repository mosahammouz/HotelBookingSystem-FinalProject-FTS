using HotelBookingSystem.Application.DTOs.Hotels;
namespace HotelBookingSystem.Application.Services;

public interface IHotelService
{
    Task<IEnumerable<HotelResponse>> GetAllAsync();
    Task<HotelResponse?> GetByIdAsync(int id);
    Task<HotelResponse> CreateAsync(CreateHotelRequest request);
    Task<HotelResponse?> UpdateAsync(int id, UpdateHotelRequest request);
    Task<bool> DeleteAsync(int id);
}