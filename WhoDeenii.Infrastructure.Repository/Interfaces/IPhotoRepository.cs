using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoDeenii.DTO.Requests;
using WhoDeenii.Infrastructure.DataAccess.Entities;

namespace WhoDeenii.Infrastructure.Repository.Interfaces
{
    public interface IPhotoRepository
    {
       
        Task AddPhotoDocumentAsync(IDDocument dDocument);
        Task<IDDocument?> GetPhotoDocumentAsync(string reservationId);
        Task<bool> ReservationExistsAsync(int reservationId);
        Task UpdatePhotoDocumentAsync(IDDocument document);
    }
}
