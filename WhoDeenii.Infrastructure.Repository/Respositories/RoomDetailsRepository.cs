using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoDeenii.DTO.Requests;
using WhoDeenii.Infrastructure.DataAccess;
using WhoDeenii.Infrastructure.DataAccess.Entities;
using WhoDeenii.Infrastructure.Repository.Interfaces;

namespace WhoDeenii.Infrastructure.Repository.Respositories
{
    public class RoomDetailsRepository : IRoomDetailsRepository
    {
        private readonly WhoDeeniiDbContext _context;

        public RoomDetailsRepository(WhoDeeniiDbContext context)
        {
            _context = context;
        }

        public async Task<RoomDetails> CreateRoomDetailsAsync(RoomDetails request)
        {
            await _context.RoomDetails.AddAsync(request);
            await _context.SaveChangesAsync();
            return request;
        }

        public async Task DeleteRoomDetailsAsync(int id)
        {
            var roomDetails = await _context.RoomDetails.FindAsync(id);
            if (roomDetails != null)
            {
                _context.RoomDetails.Remove(roomDetails);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<RoomDetails> GetRoomDetailsAsync(int id)
        {
            return await _context.RoomDetails.FindAsync(id);
        }

        public async Task UpdateRoomDetailsAsync(RoomDetails roomDetails)
        {
            _context.RoomDetails.Update(roomDetails);
            await _context.SaveChangesAsync();
        }
    }
}
