using HotelBookingSystem.Application.DTOs.Rooms;
using HotelBookingSystem.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RoomAvailabilityController : ControllerBase
{
    private readonly IRoomAvailabilityService _roomAvailabilityService;
    public RoomAvailabilityController(IRoomAvailabilityService roomAvailabilityService)
    {
        _roomAvailabilityService = roomAvailabilityService;
    }

    [HttpPost]
    public async Task<IActionResult> GetAvailableRooms([FromBody] RoomAvailabilityRequest request)
    {
        var result = await _roomAvailabilityService.GetAvailableRoomsAsync(request);
        return Ok(result);
    }
}