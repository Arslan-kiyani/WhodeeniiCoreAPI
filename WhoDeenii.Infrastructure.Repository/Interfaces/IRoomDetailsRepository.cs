using Azure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoDeenii.DTO.Requests;
using WhoDeenii.Infrastructure.DataAccess.Entities;

namespace WhoDeenii.Infrastructure.Repository.Interfaces
{
    public interface IRoomDetailsRepository
    {
        Task<RoomDetails> CreateRoomDetailsAsync(RoomDetails request);
        Task<RoomDetails> GetRoomDetailsAsync(int id);
        Task UpdateRoomDetailsAsync(RoomDetails roomDetails);
        Task DeleteRoomDetailsAsync(int id);
    }
}
