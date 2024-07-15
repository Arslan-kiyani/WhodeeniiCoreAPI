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
        private readonly ILogger<IDDocumentController> _logger;

        public IDDocumentController(IIDDocumentService service, ILogger<IDDocumentController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        [Route("api/AddIDDocument")]
        [Produces(typeof(ApiResponse<string>))]
        public async Task<IActionResult> AddIDDocument([FromForm] IDDocumentRequest idDocumentRequest)
        {
            var response = await _service.AddIDDocumentAsync(idDocumentRequest);
            return Ok(response);
        }
    }
}
