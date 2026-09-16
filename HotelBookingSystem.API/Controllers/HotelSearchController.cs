using HotelBookingSystem.Application.DTOs.Hotels_Search;
using HotelBookingSystem.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class HotelSearchController : ControllerBase
{
    private readonly IHotelSearchService _hotelSearchService;
    public HotelSearchController(IHotelSearchService hotelSearchService)
    {
        _hotelSearchService = hotelSearchService;
    }

    [HttpPost]
    public async Task<IActionResult> Search([FromBody] HotelSearchRequest request)
    {
        var result = await _hotelSearchService.SearchAsync(request);
        return Ok(result);
    }
}