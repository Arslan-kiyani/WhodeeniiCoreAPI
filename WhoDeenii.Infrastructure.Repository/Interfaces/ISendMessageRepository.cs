using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoDeenii.DTO.Requests;
using WhoDeenii.Infrastructure.DataAccess.Entities;

namespace WhoDeenii.Infrastructure.Repository.Interfaces
{
    public interface ISendMessageRepository
    {
        Task<bool> CheckReservationIdAsync(string reservationId);
        Task SendMessageAsync(WhatsAppMessage message);
        //Task CreateWhatsAppMessageAsync(WhatsAppMessageRequest message);
    }
}
