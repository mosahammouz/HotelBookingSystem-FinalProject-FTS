using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CitiesController : ControllerBase
{
    private readonly AppDbContext _dbContext;
    public CitiesController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCities()
    {
        var cities = await _dbContext.Cities.AsNoTracking().ToListAsync();
        return Ok(cities);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCityById(int id)
    {
        var existingCity = await _dbContext.Cities.FirstOrDefaultAsync(c => c.Id == id);
        if (existingCity == null) return NotFound("City Not Found");
        return Ok(existingCity);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateCity(City city)
    { 
        _dbContext.Cities.Add(city);
        await _dbContext.SaveChangesAsync();
        return CreatedAtAction(nameof(GetCityById), new { id = city.Id }, city);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateCity(int id, City updatedCity)
    {
        var city = await _dbContext.Cities.FindAsync(id);
        if (city == null)
        {
            return NotFound("City not found.");
        }

        city.Name = updatedCity.Name;
        city.Country = updatedCity.Country;
        city.PostOffice = updatedCity.PostOffice;
        city.UpdatedAt = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync();

        return Ok(city);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCity(int id)
    {
        var city = await _dbContext.Cities.FindAsync(id);
        if (city == null)
        {
            return NotFound("City not found.");
        }

        _dbContext.Cities.Remove(city);
        await _dbContext.SaveChangesAsync();
        return NoContent();
    }

}