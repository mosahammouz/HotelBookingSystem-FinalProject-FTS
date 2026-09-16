using HotelBookingSystem.Application.DTOs.Hotels;
using HotelBookingSystem.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HotelsController : ControllerBase
{
    private readonly IHotelService _hotelService;
    public HotelsController(IHotelService hotelService)
    {
        _hotelService = hotelService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllHotels()
    {
        var hotels = await _hotelService.GetAllAsync();

        return Ok(hotels);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetHotelById(int id)
    {
        var hotel = await _hotelService.GetByIdAsync(id);
        if (hotel == null) { return NotFound("Hotel not found."); }
        return Ok(hotel);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateHotel(CreateHotelRequest request)//automatically deserializes the JSON body into that C# object.
    {
        var hotel = await _hotelService.CreateAsync(request);

        return CreatedAtAction(nameof(GetHotelById), new { id = hotel.Id }, hotel);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateHotel(int id, UpdateHotelRequest request)
    {
        var hotel = await _hotelService.UpdateAsync(id, request);
        if (hotel == null) { return NotFound("Hotel not found."); }
        return Ok(hotel);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteHotel(int id)
    {
        var deleted = await _hotelService.DeleteAsync(id);
        if (!deleted) { return NotFound("Hotel not found."); }
        return NoContent();
    }
    
  
}