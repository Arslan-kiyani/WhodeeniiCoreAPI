namespace WhoDeenii.Infrastructure.DataAccess.Entities
{
    public class ProfileDetails
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsProfileUpdated { get; set; }
        public string? ReservationId { get; set; }
    }
}
