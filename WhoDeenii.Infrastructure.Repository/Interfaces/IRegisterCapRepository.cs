using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoDeenii.Infrastructure.DataAccess.Entities;

namespace WhoDeenii.Infrastructure.Repository.Interfaces
{
    public interface IRegisterCapRepository
    {
        Task AddPhotoDocumentAsync(RegistrationCard registrationCard);
        Task<RegistrationCard?> GetPhotoDocumentAsync(string reservationId);
        Task<bool> ReservationExistsAsync(int reservationId);
        Task UpdatePhotoDocumentAsync(RegistrationCard document);
    }
}
