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
        private readonly IRegisterCapService _registerCapService;

        public RegistrationCardController(IRegistrationCardService service, IRegisterCapService registerCapService)
        {
            _service = service;
            _registerCapService = registerCapService;
        }

        [HttpPost]
        [Route("api/AddRegistrationCard")]
        [Produces(typeof(ApiResponse<string>))]
        public async Task<IActionResult> AddRegistrationCard(RegistrationCardRequest registrationCardRequest)
        {
            var response = await _service.AddRegistrationCardAsync(registrationCardRequest);
            return Ok(response);
           
        }

        [HttpPost]
        [Route("CaptureRegisteration")]
        [Produces(typeof(ApiResponse<string>))]
        public async Task<IActionResult> CaptureRegisteration(CapRequest request)
        {
            var response = await _registerCapService.SaveImageAsync(request);
            return Ok(response);
        }

    }

}
