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

        public RegistrationCardRepository(WhoDeeniiDbContext context)
        {
            _context = context;
        }

        public async Task AddRegistrationCardAsync(RegistrationCard registrationCard)
        {
            await _context.AddAsync(registrationCard);
            await _context.SaveChangesAsync();
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
