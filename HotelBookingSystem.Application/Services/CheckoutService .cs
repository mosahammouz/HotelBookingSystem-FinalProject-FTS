using HotelBookingSystem.Application.DTOs.Checkout;

namespace HotelBookingSystem.Application.Services;

public class CheckoutService : ICheckoutService
{
    public async Task<CheckoutResponse> CheckoutAsync(int userId, CheckoutRequest request)
    {
        throw new NotImplementedException();
    }
}