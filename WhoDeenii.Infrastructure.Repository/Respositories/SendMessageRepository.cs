﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
    public class SendMessageRepository : ISendMessageRepository
    {
        private readonly WhoDeeniiDbContext _whoDeeniiDbContext;
        public SendMessageRepository(WhoDeeniiDbContext whoDeeniiDbContext)
        {
            _whoDeeniiDbContext = whoDeeniiDbContext;
        }

        public async Task<bool> CheckReservationIdAsync(string reservationId)
        {
            return await _whoDeeniiDbContext.Reservations.AnyAsync(r => r.ReservationId == reservationId);
        }

        public async Task SendMessageAsync(WhatsAppMessage message)
        {
            await _whoDeeniiDbContext.AddAsync(message);
            await _whoDeeniiDbContext.SaveChangesAsync();
            
        }
    }
}
