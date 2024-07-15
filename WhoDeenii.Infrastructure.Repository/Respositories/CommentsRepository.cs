using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoDeenii.DTO.Response;
using WhoDeenii.Infrastructure.DataAccess;
using WhoDeenii.Infrastructure.DataAccess.Entities;
using WhoDeenii.Infrastructure.Repository.Interfaces;

namespace WhoDeenii.Infrastructure.Repository.Respositories
{
    public class CommentsRepository : ICommentsRepository
    {

        private readonly WhoDeeniiDbContext _context;

        public CommentsRepository(WhoDeeniiDbContext context)
        {
            _context = context;
        }
        public async Task AddCommentAsync(Comments comment)
        {
            await _context.AddAsync(comment);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckReservationIdAsync(string reservationId)
        {
            return await _context.Reservations.AnyAsync(r => r.ReservationId == reservationId);
        }

        public async Task<List<Comments>> GetCommentsByReservationIdAsync(string reservationId)
        {
            //return await _context.Comments.FirstOrDefaultAsync(c => c.ReservationId == reservationId);
            return await _context.Comments.Where(c => c.ReservationId == reservationId).ToListAsync();
        }
    }
}
