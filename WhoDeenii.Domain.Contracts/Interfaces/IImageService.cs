using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoDeenii.DTO.Response;

namespace WhoDeenii.Domain.Contracts.Interfaces
{
    public interface IImageService
    {
        Task<ApiResponse<string>> GetImageAsBase64Async(string imagePath);
    }
}
