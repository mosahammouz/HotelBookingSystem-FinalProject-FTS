using HotelBookingSystem.Application.DTOs.Auth;
using HotelBookingSystem.Application.Services;
using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Domain.Enums;
using HotelBookingSystem.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _dbContext;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthController(AppDbContext dbContext, IJwtTokenService jwtTokenService)
    {
        _dbContext = dbContext;
        _jwtTokenService = jwtTokenService;
    }

    [HttpPost("register")] // api/Auth/register
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == request.Username || u.Email == request.Email );
        if (existingUser != null) { return BadRequest("Username or email already exists."); }

        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Role = UserRole.Customer
        };
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();
        return Ok(new { message = "Registration successful." });
    } 
    
    [HttpPost("login")] // api/Auth/login
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
        if (user == null) { return Unauthorized("Invalid username or password."); }

        var passwordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

        if (!passwordValid)
        {
            return Unauthorized("Invalid username or password.");
        }

        var token = _jwtTokenService.GenerateToken(user);

        return Ok(new { token });
    }
}
