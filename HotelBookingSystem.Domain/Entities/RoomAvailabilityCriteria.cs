namespace HotelBookingSystem.Domain.Entities;

public class RoomAvailabilityCriteria
{
    public int HotelId { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
}