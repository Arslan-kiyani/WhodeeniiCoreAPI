using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WhoDeenii.Domain.Contracts.Interfaces;
using WhoDeenii.DTO.Requests;
using WhoDeenii.DTO.Response;
using WhoDeenii.Infrastructure.DataAccess.Entities;

namespace WhodeeniiCoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendController : ControllerBase
    {
        private readonly ISendMessageService _service;

        public SendController(ISendMessageService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("Send")]
        [Produces(typeof(ApiResponse<string>))]
        public async Task<ActionResult> Send([FromForm] WhatsAppMessageRequest message)
        {
            var response = await _service.SendMessageAsync(message);
            return Ok(response);
            
        }
    }
}
