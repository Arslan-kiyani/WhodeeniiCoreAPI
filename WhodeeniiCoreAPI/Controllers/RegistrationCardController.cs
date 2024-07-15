using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WhoDeenii.Domain.Contracts.Interfaces;
using WhoDeenii.DTO.Requests;
using WhoDeenii.DTO.Response;

namespace WhodeeniiCoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationCardController : ControllerBase
    {
        private readonly IRegistrationCardService _service;
        private readonly ILogger<RegistrationCardController> _logger;

        public RegistrationCardController(IRegistrationCardService service, ILogger<RegistrationCardController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        [Route("api/AddRegistrationCard")]
        [Produces(typeof(ApiResponse<string>))]
        public async Task<IActionResult> AddRegistrationCard([FromForm] RegistrationCardRequest registrationCardRequest)
        {
            var response = await _service.AddRegistrationCardAsync(registrationCardRequest);
            return Ok(response);
           
        }
    }

}
