using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoDeenii.DTO.Requests;
using WhoDeenii.DTO.Response;
using WhoDeenii.Infrastructure.DataAccess.Entities;

namespace WhoDeenii.Domain.Contracts.Interfaces
{
    public interface IAttachedDocumentService
    {
        Task<ApiResponse<string>> DeleteAttachedDocumentAsync(int id);
        Task<ApiResponse<List<AttachDocuments>>> ReservationByReservationIdAsync(string reservationId);
        Task<ApiResponse<string>> UploadFileAsync( AttachDocumentsRequest request);
    }
}
