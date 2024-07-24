using Microsoft.EntityFrameworkCore;
using WhoDeenii.Infrastructure.DataAccess.Entities;

namespace WhoDeenii.Infrastructure.DataAccess
{
    public class WhoDeeniiDbContext : DbContext 
    {
        public WhoDeeniiDbContext(DbContextOptions<WhoDeeniiDbContext> options) : base(options)
        {
        }

        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ProfileDetails> ProfileDetails { get; set; }
        public DbSet<RegistrationCard> RegistrationCards { get; set; }
        public DbSet<IDDocument> IDDocuments { get; set; }
        public DbSet<WhatsAppMessage> WhatsAppMessages { get; set;}
        public DbSet<SmsMessage> smsMessages { get; set; }

        public DbSet<GetReservationDetails> GetReservationDetails { get; set; }
        public DbSet<AttachDocuments> attachDocuments {  get; set; } 
        public DbSet<Comments> Comments { get; set; }
        public DbSet<LogEntry> Logs { get; set; }

        public DbSet<RoomDetails> RoomDetails { get; set; }

    }
}
