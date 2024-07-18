using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WhoDeenii.Domain.Contracts.Interfaces;
using WhoDeenii.Domain.Services.Services;
using WhoDeenii.DTO.Requests;
using WhoDeenii.DTO.Response;

namespace WhodeeniiCoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaptureController : ControllerBase
    {
        private readonly IPhotoService _photoService;

        public CaptureController(IPhotoService photoService)
        {
            _photoService = photoService;
        }
        [HttpPost]
        [Route("CaptureImage")]
        [Produces(typeof(ApiResponse<string>))]
        public async Task<IActionResult> CaptureImage( CapRequest request)
        {
            
          var response =  await _photoService.SaveImageAsync(request);
            return Ok(response);
        }
    }
}
