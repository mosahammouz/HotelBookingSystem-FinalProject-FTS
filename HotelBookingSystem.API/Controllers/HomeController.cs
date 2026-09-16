using System.Security.Claims;
using HotelBookingSystem.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingSystem.API.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class HomeController : ControllerBase
{
    private readonly IHomeService _homeService;
    public HomeController(IHomeService homeService)
    {
        _homeService = homeService;
    }

    [HttpGet("featured-deals")] // api/Home/featured-deals
    public async Task<IActionResult> GetFeaturedDeals()
    {
        var result = await _homeService.GetFeaturedDealsAsync();
        if (result.Count == 0) { return Ok(new { message = "No feature deals found." }); }  // to be more user-friendly
        return Ok(result);
    }

    [HttpGet("recently-visited")]// api/Home/recently-visited
    public async Task<IActionResult> GetRecentlyVisited()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null) { return Unauthorized(); }
        var userId = int.Parse(userIdClaim.Value);
        var result = await _homeService.GetRecentlyVisitedAsync(userId);
        if (result.Count == 0) { return Ok(new { message = "No recently visited cities found." }); }  

        return Ok(result);
    }

    [HttpGet("trending-cities")]// api/Home/trending-cities
    public async Task<IActionResult> GetTrendingCities()
    {
        var result = await _homeService.GetTrendingCitiesAsync();
        if (result.Count == 0) { return Ok(new { message = "No trending cities found." }); }  
        return Ok(result);
    }
}