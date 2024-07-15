using AutoMapper;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WhoDeenii.Infrastructure.DataAccess;
using WhoDeenii.Infrastructure.DataAccess.Entities;
using WhoDeenii.Infrastructure.Repository.Interfaces;


namespace WhoDeenii.Infrastructure.Repository.Respositories
{
    public class RegistrationCardRepository : IRegistrationCardRepository
    {
        private readonly WhoDeeniiDbContext _context;
        private readonly ILogger<RegistrationCardRepository> _logger;

        public RegistrationCardRepository(WhoDeeniiDbContext context, ILogger<RegistrationCardRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddRegistrationCardAsync(RegistrationCard registrationCard)
        {
            try
            {
                await _context.AddAsync(registrationCard);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding registration card");
               
            }
        }

        public async Task<bool> CheckReservationIdAsync(string reservationId)
        {
            return await _context.Reservations.AnyAsync(r => r.ReservationId == reservationId);
        }

        public async Task<RegistrationCard?> GetRegistrationCardByReservationIdAsync(string reservationId)
        {
            return await _context.RegistrationCards.FirstOrDefaultAsync(rc => rc.ReservationId == reservationId);
        }

        public async Task UpdateRegistrationCardAsync(RegistrationCard existingRegistrationCard)
        {
            _context.RegistrationCards.Update(existingRegistrationCard);
            await _context.SaveChangesAsync();
        }
    }

}
