using System.Security.Claims;
using HotelBookingSystem.Application.DTOs.Checkout;
using HotelBookingSystem.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingSystem.API.Controllers;

[ApiController]
[Route("api/bookings")]
[Authorize]
public class CheckoutController : ControllerBase
{
    private readonly ICheckoutService _checkoutService;
    public CheckoutController(ICheckoutService checkoutService)
    {
        _checkoutService = checkoutService;
    }

    [HttpPost("checkout")]
    public async Task<IActionResult> Checkout([FromBody] CheckoutRequest request)//hotelId , RoomId , in, out , paymentMethod
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);//Look inside the currently authenticated user's JWT claims and find the claim that represents their ID.
        if (userIdClaim == null) { return Unauthorized(); }// not logged in
        var userId = int.Parse(userIdClaim.Value); // cuz it comes from JWT claims not from body
        var result = await _checkoutService.CheckoutAsync(userId, request);
        return Ok(result);
    }
}