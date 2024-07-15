namespace WhoDeenii.Infrastructure.DataAccess.Entities
{
    public class IDDocument
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string ReservationId { get; set; }
    }
}
