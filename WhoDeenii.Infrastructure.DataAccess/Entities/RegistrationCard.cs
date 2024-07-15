namespace WhoDeenii.Infrastructure.DataAccess.Entities
{
    public class RegistrationCard
    {
        public int Id { get; set; }
        public string Imagepath { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string ReservationId { get; set; }
    }
}
