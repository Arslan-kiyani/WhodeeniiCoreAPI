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
    public interface IRoomDetailsService
    {
        Task<ApiResponse<string>> CreateRoomDetailsAsync(RoomDetailsRequest request);
        Task<RoomDetails> GetRoomDetailsAsync(int id);
        Task UpdateRoomDetailsAsync(int id ,RoomDetails roomDetails);
        Task DeleteRoomDetailsAsync(int id);
    }
}
