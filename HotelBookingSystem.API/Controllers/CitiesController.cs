using HotelBookingSystem.Application.DTOs.Cities;
using HotelBookingSystem.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CitiesController : ControllerBase
{
    private readonly ICityService _cityService;
    public CitiesController(ICityService cityService)
    {
        _cityService = cityService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCities()
    {
        var cities = await _cityService.GetAllAsync();
        return Ok(cities);
    }
    
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCityById(int id)
    {
        var city = await _cityService.GetByIdAsync(id);
        if (city == null) { return NotFound("City not found."); }
        return Ok(city);
    }
    
    
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateCity(CreateCityRequest request)
    {
        var city = await _cityService.CreateAsync(request);
        return CreatedAtAction(nameof(GetCityById), new { id = city.Id }, city);
    }
    
    
    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateCity(int id, UpdateCityRequest request)
    {
        var city = await _cityService.UpdateAsync(id, request);
        if (city == null) { return NotFound("City not found."); }
        return Ok(city);
    }
    
    
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCity(int id)
    {
        var deleted = await _cityService.DeleteAsync(id);
        if (!deleted) {return NotFound("City not found."); }
        return NoContent();
    }

}