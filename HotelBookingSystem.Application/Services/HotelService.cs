using HotelBookingSystem.Application.DTOs.Hotels;
using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Domain.Interfaces;

namespace HotelBookingSystem.Application.Services;

public class HotelService : IHotelService
{
    private readonly IHotelRepository _hotelRepository;
    public HotelService(IHotelRepository hotelRepository)
    {
        _hotelRepository = hotelRepository;
    }

    public async Task<IEnumerable<HotelResponse>> GetAllAsync()
    {
        var hotels = await _hotelRepository.GetAllAsync();
        return hotels.Select(MapToResponse);
    }

    public async Task<HotelResponse?> GetByIdAsync(int id)
    {
        var hotel = await _hotelRepository.GetByIdAsync(id);
        if (hotel == null) { return null; }
        return MapToResponse(hotel);
    }

    public async Task<HotelResponse> CreateAsync(CreateHotelRequest request)
    {
        var hotel = new Hotel
        {
            Name = request.Name,
            Description = request.Description,
            StarRating = request.StarRating,
            CityId = request.CityId,
            OwnerId = request.OwnerId,
            Location = request.Location,
            Latitude = request.Latitude,
            Longitude = request.Longitude
        };

        await _hotelRepository.AddAsync(hotel);

        // Get the hotel again so City and Owner are loaded
        var createdHotel = await _hotelRepository.GetByIdAsync(hotel.Id);
        return MapToResponse(createdHotel!);
    }

    public async Task<HotelResponse?> UpdateAsync(int id, UpdateHotelRequest request)
    {
        var hotel = await _hotelRepository.GetByIdAsync(id);
        if (hotel == null) { return null; }

        hotel.Name = request.Name;
        hotel.Description = request.Description;
        hotel.StarRating = request.StarRating;
        hotel.CityId = request.CityId;
        hotel.OwnerId = request.OwnerId;
        hotel.Location = request.Location;
        hotel.Latitude = request.Latitude;
        hotel.Longitude = request.Longitude;
        hotel.UpdatedAt = DateTime.UtcNow;

        await _hotelRepository.UpdateAsync(hotel);
        return MapToResponse(hotel);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var hotel = await _hotelRepository.GetByIdAsync(id);
        if (hotel == null) { return false; }
       await _hotelRepository.DeleteAsync(hotel);
        return true;
    }

    private static HotelResponse MapToResponse(Hotel hotel) //DRY
    {
        return new HotelResponse
        {
            Id = hotel.Id,
            Name = hotel.Name,
            Description = hotel.Description,
            StarRating = hotel.StarRating,

            CityId = hotel.CityId,
            CityName = hotel.City.Name,

            OwnerId = hotel.OwnerId,
            OwnerUsername = hotel.Owner.Username,

            Location = hotel.Location,
            Latitude = hotel.Latitude,
            Longitude = hotel.Longitude
        };
    }
    
}