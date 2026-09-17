using HotelBookingSystem.Application.DTOs.Rooms;
using HotelBookingSystem.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingSystem.API.Controllers;

[ApiController]
[Route("api/hotels")]
[Authorize]
public class RoomAvailabilityController : ControllerBase
{
    private readonly IRoomAvailabilityService _roomAvailabilityService;
    public RoomAvailabilityController(IRoomAvailabilityService roomAvailabilityService)
    {
        _roomAvailabilityService = roomAvailabilityService;
    }

    [HttpPost("rooms/availability")]
    public async Task<IActionResult> GetAvailableRooms([FromBody] RoomAvailabilityRequest request)  //HotelId , check-in and check-out
    {
        var result = await _roomAvailabilityService.GetAvailableRoomsAsync(request);
        return Ok(result);
    }
}