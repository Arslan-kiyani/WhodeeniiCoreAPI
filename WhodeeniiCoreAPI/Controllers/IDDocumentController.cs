using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WhoDeenii.Domain.Contracts.Interfaces;
using WhoDeenii.DTO.Requests;
using WhoDeenii.DTO.Response;

namespace WhodeeniiCoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IDDocumentController : ControllerBase
    {
        private readonly IIDDocumentService _service;
        private readonly IPhotoService _photoService;
        public IDDocumentController(IIDDocumentService service, IPhotoService photoService)
        {
            _service = service;
            _photoService = photoService;
        }

        [HttpPost]
        [Route("api/AddIDDocument")]
        [Produces(typeof(ApiResponse<string>))]
        public async Task<IActionResult> AddIDDocument(IDDocumentRequest request)
        {
            var response = await _service.AddIDDocumentAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("CaptureImage")]
        [Produces(typeof(ApiResponse<string>))]
        public async Task<IActionResult> CaptureImage(CapRequest request)
        {
            var response = await _photoService.SaveImageAsync(request);
            return Ok(response);
        }
    }
}
