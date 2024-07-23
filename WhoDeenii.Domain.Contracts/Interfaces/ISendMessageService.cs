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
    public interface ISendMessageService
    {
        Task<ApiResponse<string>> SendMessageAsync(WhatsAppMessageRequest request);
    }
}
