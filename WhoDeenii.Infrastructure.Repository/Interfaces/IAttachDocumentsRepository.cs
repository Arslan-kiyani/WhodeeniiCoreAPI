using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoDeenii.DTO.Requests;
using WhoDeenii.Infrastructure.DataAccess.Entities;

namespace WhoDeenii.Infrastructure.Repository.Interfaces
{
    public interface IAttachDocumentsRepository
    {
        Task<bool> AddDocumentAsync(AttachDocuments document);
        Task<bool> CheckReservationIdAsync(string? reservationId);
        Task<bool> DeleteAttachedDocumentAsync(int id );
        Task<List<AttachDocuments>> GetByReservationIdAsync(string reservationId);
    }
}
