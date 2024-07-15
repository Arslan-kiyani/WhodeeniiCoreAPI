using WhoDeenii.DTO.Requests;
using WhoDeenii.DTO.Response;

namespace WhoDeenii.Domain.Contracts.Interfaces
{
    public interface IRegistrationCardService
    {
        Task<ApiResponse<string>> AddRegistrationCardAsync(RegistrationCardRequest registrationCardRequest);
    }
}
