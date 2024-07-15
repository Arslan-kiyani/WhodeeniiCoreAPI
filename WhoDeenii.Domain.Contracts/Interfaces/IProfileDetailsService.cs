

using WhoDeenii.DTO.Requests;
using WhoDeenii.DTO.Response;

namespace WhoDeenii.Domain.Contracts.Interfaces
{
    public interface IProfileDetailsService
    {
        Task<ApiResponse<string>> AddProfileDetailsAsync(ProfileDetailRequest ProfileDetailRequest);
    }
}
