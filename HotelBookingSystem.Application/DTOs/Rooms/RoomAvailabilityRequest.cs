namespace HotelBookingSystem.Application.DTOs.Rooms;

public class RoomAvailabilityRequest
{
    public int HotelId { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
}