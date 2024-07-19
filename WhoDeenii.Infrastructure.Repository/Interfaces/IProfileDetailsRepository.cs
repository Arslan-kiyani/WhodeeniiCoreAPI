

using WhoDeenii.Infrastructure.DataAccess.Entities;

namespace WhoDeenii.Infrastructure.Repository.Interfaces
{
    public interface IProfileDetailsRepository
    {
        //Task AddProfileDetailsAsync(ProfileDetailRequest ProfileDetailRequest);
        Task AddProfileDetailsAsync(ProfileDetails profile);
        Task<ProfileDetails?> GetByReservationIdAsync(string reservationId);
    }
}
