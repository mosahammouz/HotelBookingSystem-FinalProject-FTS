using HotelBookingSystem.Domain.Interfaces;
using HotelBookingSystem.Infrastructure.Email;
using HotelBookingSystem.Infrastructure.Persistence;
using HotelBookingSystem.Infrastructure.Persistence.Repositories;
using HotelBookingSystem.Infrastructure.Third_library_services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotelBookingSystem.Infrastructure.DependencyInjection;

public static class InfrastructureRegister
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options => // It's Scoped by default
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<ICityRepository, CityRepository>();
        services.AddScoped<IHotelRepository, HotelRepository>();
        services.AddScoped<IRoomRepository, RoomRepository>();
        services.AddScoped<IHomeRepository, HomeRepository>();
        services.AddScoped<IHotelSearchRepository, HotelSearchRepository>();
        services.AddScoped<IRoomAvailabilityRepository, RoomAvailabilityRepository>();
        services.AddScoped<ICheckoutRepository, CheckoutRepository>();
        services.AddScoped<IConfirmationRepository, ConfirmationRepository>();

        services.AddScoped<IConfirmationPdfGenerator, ConfirmationPdfGenerator>();
        services.AddScoped<IEmailSender, EmailSender>();

        return services;
    }
}