
using WhoDeenii.Infrastructure.DataAccess.Entities;


namespace WhoDeenii.Infrastructure.Repository.Interfaces
{
    public interface IRegistrationCardRepository
    {
        Task<bool> CheckReservationIdAsync(string reservationId);
        Task AddRegistrationCardAsync(RegistrationCard registrationCard);
        Task<RegistrationCard> GetRegistrationCardByReservationIdAsync(string reservationId);
        Task UpdateRegistrationCardAsync(RegistrationCard existingRegistrationCard);
    }
}
