using HotelBookingSystem.Domain.Entities;
namespace HotelBookingSystem.Application.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user);
}