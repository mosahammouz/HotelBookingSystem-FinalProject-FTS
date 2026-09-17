using HotelBookingSystem.Domain.Entities;

namespace HotelBookingSystem.Domain.Interfaces;

public interface IConfirmationRepository
{
    Task<Booking?> GetBookingForConfirmationAsync(int userId, int bookingId);
}