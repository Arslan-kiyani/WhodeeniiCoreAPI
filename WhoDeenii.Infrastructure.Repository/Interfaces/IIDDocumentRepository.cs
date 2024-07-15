using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoDeenii.Infrastructure.DataAccess.Entities;


namespace WhoDeenii.Infrastructure.Repository.Interfaces
{
    public interface IIDDocumentRepository
    {
        Task AddIDDocumentAsync(IDDocument idDocument);
        Task UpdateIDDocumentAsync(IDDocument idDocument);
        Task<IDDocument> GetIDDocumentByReservationIdAsync(string reservationId);
        Task<bool> CheckReservationIdAsync(string reservationId);
    }
}
