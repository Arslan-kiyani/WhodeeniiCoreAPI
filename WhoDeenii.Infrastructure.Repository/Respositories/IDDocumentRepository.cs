using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WhoDeenii.Infrastructure.DataAccess;
using WhoDeenii.Infrastructure.DataAccess.Entities;
using WhoDeenii.Infrastructure.Repository.Interfaces;


namespace WhoDeenii.Infrastructure.Repository.Respositories
{
    public class IDDocumentRepository : IIDDocumentRepository
    {
        private readonly WhoDeeniiDbContext _context;
        private readonly ILogger<IDDocumentRepository> _logger;

        public IDDocumentRepository(WhoDeeniiDbContext context, ILogger<IDDocumentRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddIDDocumentAsync(IDDocument idDocument)
        {
            try
            {
                await _context.AddAsync(idDocument);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding ID document");
                
            }
        }

        public async Task<bool> CheckReservationIdAsync(string reservationId)
        {
            return await _context.Reservations.AnyAsync(r => r.ReservationId == reservationId);
        }

        public async Task<IDDocument> GetIDDocumentByReservationIdAsync(string reservationId)
        {
            return await _context.IDDocuments.FirstOrDefaultAsync(d => d.ReservationId == reservationId);
        }

        public async Task UpdateIDDocumentAsync(IDDocument idDocument)
        {
            _context.IDDocuments.Update(idDocument);
            await _context.SaveChangesAsync();
        }
    }
}
