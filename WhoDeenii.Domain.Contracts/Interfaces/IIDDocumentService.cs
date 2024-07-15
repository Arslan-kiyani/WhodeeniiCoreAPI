
using WhoDeenii.DTO.Requests;
using WhoDeenii.DTO.Response;

namespace WhoDeenii.Domain.Contracts.Interfaces
{
    public interface IIDDocumentService
    {
        Task<ApiResponse<string>> AddIDDocumentAsync(IDDocumentRequest idDocumentRequest);
    }
}
