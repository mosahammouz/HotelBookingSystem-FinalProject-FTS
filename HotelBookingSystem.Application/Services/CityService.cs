using HotelBookingSystem.Application.DTOs.Cities;
using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Domain.Interfaces;

namespace HotelBookingSystem.Application.Services;

public class CityService : ICityService
{
    private readonly ICityRepository _cityRepository;
    public CityService(ICityRepository cityRepository)
    {
        _cityRepository = cityRepository;
    }

    public async Task<IEnumerable<CityResponse>> GetAllAsync()
    {
        var cities = await _cityRepository.GetAllAsync();

        return cities.Select(city => new CityResponse  // for DTO purposes
        {
            Id = city.Id,
            Name = city.Name,
            Country = city.Country,
            PostOffice = city.PostOffice
        });
    }

    public async Task<CityResponse?> GetByIdAsync(int id)
    {
        var city = await _cityRepository.GetByIdAsync(id);
        if (city == null) { return null; }
        return MapToResponse(city);// for DTO purposes
    }

    public async Task<CityResponse> CreateAsync(CreateCityRequest request)
    {
        var city = new City
        {
            Name = request.Name,
            Country = request.Country,
            PostOffice = request.PostOffice
        };

        await _cityRepository.AddAsync(city);

        return MapToResponse(city);// for DTO purposes
    }

    public async Task<CityResponse?> UpdateAsync(int id, UpdateCityRequest request)
    {
        var city = await _cityRepository.GetByIdAsync(id);
        if (city == null) { return null; }

        city.Name = request.Name;
        city.Country = request.Country;
        city.PostOffice = request.PostOffice;
        city.UpdatedAt = DateTime.UtcNow;

        await _cityRepository.UpdateAsync(city);

        return MapToResponse(city);// for DTO purposes
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var city = await _cityRepository.GetByIdAsync(id);
        if (city == null) { return false; }
        await _cityRepository.DeleteAsync(city);

        return true;
    }

    private static CityResponse MapToResponse(City city)
    {
        return new CityResponse
        {
            Id = city.Id,
            Name = city.Name,
            Country = city.Country,
            PostOffice = city.PostOffice
        };
    }
}