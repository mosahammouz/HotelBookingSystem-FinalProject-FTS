using System.Security.Claims;
using HotelBookingSystem.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingSystem.API.Controllers;

[ApiController]
[Route("api/bookings")]
[Authorize]
public class ConfirmationController : ControllerBase
{
    private readonly IConfirmationService _confirmationService;
    public ConfirmationController(
        IConfirmationService confirmationService)
    {
        _confirmationService = confirmationService;
    }

    [HttpGet("{bookingId:int}/confirmation")]
    public async Task<IActionResult> GetConfirmation(int bookingId)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null) { return Unauthorized(); }
        var userId = int.Parse(userIdClaim.Value);
        var result = await _confirmationService.GetConfirmationAsync(userId, bookingId);
        if (result == null) { return NotFound("Booking confirmation not found."); }
        return Ok(result);
    }
}