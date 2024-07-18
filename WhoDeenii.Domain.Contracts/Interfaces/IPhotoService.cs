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
    public interface IPhotoService
    {
        Task<ApiResponse<string>> SaveImageAsync(CapRequest request);
    }
}
