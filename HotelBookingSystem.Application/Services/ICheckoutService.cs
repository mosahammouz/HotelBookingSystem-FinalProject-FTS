using HotelBookingSystem.Application.DTOs.Checkout;

namespace HotelBookingSystem.Application.Services;

public interface ICheckoutService
{
    Task<CheckoutResponse> CheckoutAsync(int userId, CheckoutRequest request);
}