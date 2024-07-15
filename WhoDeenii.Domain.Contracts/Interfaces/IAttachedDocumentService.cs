using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoDeenii.DTO.Requests;
using WhoDeenii.DTO.Response;

namespace WhoDeenii.Domain.Contracts.Interfaces
{
    public interface IAttachedDocumentService
    {
        Task<ApiResponse<string>> UploadFileAsync(IFormFile file);
    }
}
