namespace WhoDeenii.Infrastructure.DataAccess.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public string? ReservationId { get; set; }
        public string? GuestName { get; set; }
        public string? HotelName { get; set; }
        
    }
}
