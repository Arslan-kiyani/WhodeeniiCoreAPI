using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WhoDeenii.DTO.Requests;
using WhoDeenii.Infrastructure.DataAccess;
using WhoDeenii.Infrastructure.DataAccess.Entities;
using WhoDeenii.Infrastructure.Repository.Interfaces;

namespace WhoDeenii.Infrastructure.Repository.Respositories
{
    public class PhotoRepository : IPhotoRepository
    {

        private readonly WhoDeeniiDbContext _dbContext;

        public PhotoRepository(WhoDeeniiDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task AddPhotoDocumentAsync(IDDocument dDocument)
        {
            await _dbContext.IDDocuments.AddAsync(dDocument);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IDDocument?> GetPhotoDocumentAsync(string reservationId)
        {
            return await _dbContext.IDDocuments.FirstOrDefaultAsync(d => d.ReservationId == reservationId);
        }

        public async Task<bool> ReservationExistsAsync(int reservationId)
        {
            return await _dbContext.Reservations.AnyAsync(r => r.Id == reservationId);
        }

        public async Task UpdatePhotoDocumentAsync(IDDocument document)
        {
            _dbContext.IDDocuments.Update(document);
            await _dbContext.SaveChangesAsync();
        }
    }
}
