using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Domain.Enums;
using HotelBookingSystem.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HotelBookingSystem.Tests.Integration;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            var connectionString =
                Environment.GetEnvironmentVariable("HOTEL_TEST_DB_CONNECTION")
                ?? throw new InvalidOperationException(
                    "HOTEL_TEST_DB_CONNECTION environment variable is not set.");

            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connectionString));
        });
    }

    public async Task SeedTestDataAsync()
    {
        using var scope = Services.CreateScope();

        var dbContext = scope.ServiceProvider
            .GetRequiredService<AppDbContext>();

        var uniqueId = Guid.NewGuid().ToString("N")[..8];

        var user = new User
        {
            Username = $"integration_test_user_{uniqueId}",
            Email = $"integration_{uniqueId}@test.com",
            PasswordHash = "test-password",
            Role = UserRole.Customer
        };

        var city = new City
        {
            Name = $"Test City {uniqueId}"
        };

        dbContext.Users.Add(user);
        dbContext.Cities.Add(city);

        await dbContext.SaveChangesAsync();

        var hotel = new Hotel
        {
            Name = $"Integration Test Hotel {uniqueId}",
            Description = "Hotel used for integration tests",
            StarRating = 4,
            CityId = city.Id,
            OwnerId = user.Id,
            Location = "Test Location",
            Latitude = 32.2211,
            Longitude = 35.2544
        };

        dbContext.Hotels.Add(hotel);

        await dbContext.SaveChangesAsync();

        var room = new Room
        {
            HotelId = hotel.Id,
            RoomNumber = $"TEST-{uniqueId}",
            RoomType = RoomType.Standard,
            AdultCapacity = 2,
            ChildrenCapacity = 1,
            PricePerNight = 100,
            IsAvailable = true
        };

        dbContext.Rooms.Add(room);

        await dbContext.SaveChangesAsync();
    }
}