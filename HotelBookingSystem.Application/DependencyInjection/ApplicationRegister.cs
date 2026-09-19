using HotelBookingSystem.Application.Services;
using Microsoft.Extensions.DependencyInjection;
namespace HotelBookingSystem.Application.DependencyInjection;

public static class ApplicationRegister
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<ICityService, CityService>();
        services.AddScoped<IHotelService, HotelService>();
        services.AddScoped<IRoomService, RoomService>();
        services.AddScoped<IHomeService, HomeService>();
        services.AddScoped<IHotelSearchService, HotelSearchService>();
        services.AddScoped<IRoomAvailabilityService, RoomAvailabilityService>();
        services.AddScoped<ICheckoutService, CheckoutService>();
        services.AddScoped<IConfirmationService, ConfirmationService>();
        services.AddScoped<IConfirmationPdfService, ConfirmationPdfService>();
        services.AddScoped<IConfirmationEmailService, ConfirmationEmailService>();

        return services;
    }
}