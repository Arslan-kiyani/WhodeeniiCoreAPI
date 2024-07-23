

using WhoDeenii.DTO.Requests;
using WhoDeenii.DTO.Response;
using WhoDeenii.Infrastructure.DataAccess.Entities;

namespace WhoDeenii.Domain.Contracts.Interfaces
{
    public interface IProfileDetailsService
    {
        Task<ApiResponse<string>> AddProfileDetailsAsync(ProfileDetailRequest request);
        Task<ApiResponse<ProfileDetails>> GetProfileDetailsByReservationIdAsync(string reservationId);
    }
}
