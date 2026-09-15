using HotelBookingSystem.Application.DTOs.Rooms;
using HotelBookingSystem.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingSystem.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    private readonly IRoomService _roomService;
    public RoomsController(IRoomService roomService)
    {
        _roomService = roomService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRooms()
    {
        var rooms = await _roomService.GetAllAsync();
        return Ok(rooms);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetRoomById(int id)
    {
        var room = await _roomService.GetByIdAsync(id);
        if (room == null) { return NotFound("Room not found."); }
        return Ok(room);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateRoom(
        CreateRoomRequest request)
    {
        var room = await _roomService.CreateAsync(request);
        return CreatedAtAction(nameof(GetRoomById), new { id = room.Id }, room);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateRoom(
        int id,
        UpdateRoomRequest request)
    {
        var room = await _roomService.UpdateAsync(id, request);
        if (room == null)
        { return NotFound("Room not found."); }
        return Ok(room);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteRoom(int id)
    {
        var deleted = await _roomService.DeleteAsync(id);
        if (!deleted) { return NotFound("Room not found."); }
        return NoContent();
    }
}