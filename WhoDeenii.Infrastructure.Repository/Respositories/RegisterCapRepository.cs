using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoDeenii.Infrastructure.DataAccess;
using WhoDeenii.Infrastructure.DataAccess.Entities;
using WhoDeenii.Infrastructure.Repository.Interfaces;

namespace WhoDeenii.Infrastructure.Repository.Respositories
{
    public class RegisterCapRepository : IRegisterCapRepository
    {

        private readonly WhoDeeniiDbContext _dbContext;

        public RegisterCapRepository(WhoDeeniiDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddPhotoDocumentAsync(RegistrationCard registrationCard)
        {
            await _dbContext.RegistrationCards.AddAsync(registrationCard);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<RegistrationCard?> GetPhotoDocumentAsync(string reservationId)
        {
            return await _dbContext.RegistrationCards.FirstOrDefaultAsync(d => d.ReservationId == reservationId);
        }

        public async Task<bool> ReservationExistsAsync(int reservationId)
        {
            return await _dbContext.Reservations.AnyAsync(r => r.Id == reservationId);
        }

        public async Task UpdatePhotoDocumentAsync(RegistrationCard document)
        {
            _dbContext.RegistrationCards.Update(document);
            await _dbContext.SaveChangesAsync();
        }
    }
}
