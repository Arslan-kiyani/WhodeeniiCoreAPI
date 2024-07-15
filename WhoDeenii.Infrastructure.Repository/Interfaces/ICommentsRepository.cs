using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoDeenii.DTO.Response;
using WhoDeenii.Infrastructure.DataAccess.Entities;

namespace WhoDeenii.Infrastructure.Repository.Interfaces
{
    public interface ICommentsRepository
    {
        Task AddCommentAsync(Comments comment);
        Task<bool> CheckReservationIdAsync(string reservationId);
        Task<List<Comments>> GetCommentsByReservationIdAsync(string reservationId);

    }
}
