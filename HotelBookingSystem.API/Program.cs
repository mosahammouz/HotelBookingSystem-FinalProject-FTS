using System.Text;
using HotelBookingSystem.Application.Configuration;
using HotelBookingSystem.Application.Services;
using HotelBookingSystem.Domain.Interfaces;
using HotelBookingSystem.Infrastructure.Email;
using HotelBookingSystem.Infrastructure.Persistence;
using HotelBookingSystem.Infrastructure.Persistence.Repositories;
using HotelBookingSystem.Infrastructure.Third_library_services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuestPDF.Infrastructure;
var builder = WebApplication.CreateBuilder(args);
QuestPDF.Settings.License = LicenseType.Evaluation;
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options => // It's Scoped by default
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<IHotelRepository, HotelRepository>();
builder.Services.AddScoped<IHotelService, HotelService>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IHomeRepository, HomeRepository>();
builder.Services.AddScoped<IHomeService, HomeService>();
builder.Services.AddScoped<IHotelSearchService, HotelSearchService>();
builder.Services.AddScoped<IHotelSearchRepository, HotelSearchRepository>();
builder.Services.AddScoped<IRoomAvailabilityRepository, RoomAvailabilityRepository>();
builder.Services.AddScoped<IRoomAvailabilityService, RoomAvailabilityService>();
builder.Services.AddScoped<ICheckoutRepository, CheckoutRepository>();
builder.Services.AddScoped<ICheckoutService, CheckoutService>();
builder.Services.AddScoped<IConfirmationService, ConfirmationService>();
builder.Services.AddScoped<IConfirmationRepository, ConfirmationRepository>();
builder.Services.AddScoped<IConfirmationPdfService, ConfirmationPdfService>();
builder.Services.AddScoped<IConfirmationPdfGenerator, ConfirmationPdfGenerator>();
builder.Services.AddScoped<IEmailSender, EmailSender>();

builder.Services.AddScoped<IConfirmationEmailService, ConfirmationEmailService>();
var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>() ?? throw new InvalidOperationException("JWT settings are missing.");
builder.Services.AddSingleton(jwtSettings); // registering it in the DI container

// JWT authentication
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings.Key))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();
app.UseCors("Frontend");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGet("/", () => "Server is running ...");
app.Run();

public partial class Program
{
}