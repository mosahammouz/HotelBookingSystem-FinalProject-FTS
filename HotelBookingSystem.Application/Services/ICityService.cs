using HotelBookingSystem.Application.DTOs.Cities;
namespace HotelBookingSystem.Application.Services;

public interface ICityService
{
    Task<IEnumerable<CityResponse>> GetAllAsync();
    Task<CityResponse?> GetByIdAsync(int id);
    Task<CityResponse> CreateAsync(CreateCityRequest request);
    Task<CityResponse?> UpdateAsync(int id, UpdateCityRequest request);
    Task<bool> DeleteAsync(int id);
}