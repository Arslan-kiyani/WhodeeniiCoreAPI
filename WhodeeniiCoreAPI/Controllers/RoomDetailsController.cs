using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WhoDeenii.Domain.Contracts.Interfaces;
using WhoDeenii.DTO.Requests;
using WhoDeenii.Infrastructure.DataAccess.Entities;

namespace WhodeeniiCoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomDetailsController : ControllerBase
    {
        private readonly IRoomDetailsService _roomDetailsService;

        public RoomDetailsController(IRoomDetailsService roomDetailsService)
        {
            _roomDetailsService = roomDetailsService;
        }

        [HttpPost]
       
        public async Task<ActionResult> CreateRoomDetails(RoomDetailsRequest request)
        {
            var createdRoomDetails = await _roomDetailsService.CreateRoomDetailsAsync(request);
            return Ok(createdRoomDetails);
        }

        [HttpPut("{id}")]
        
        public async Task<IActionResult> UpdateRoomDetails(int id, RoomDetails roomDetails)
        {
            
            await _roomDetailsService.UpdateRoomDetailsAsync(id,roomDetails);
            return Ok();
        }

        [HttpDelete("{id}")]
        
        public async Task<IActionResult> DeleteRoomDetails(int id)
        {
            await _roomDetailsService.DeleteRoomDetailsAsync(id);
            return NoContent();
        }
    }
}
