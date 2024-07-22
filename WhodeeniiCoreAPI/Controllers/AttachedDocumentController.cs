using FluentAssertions.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WhoDeenii.Domain.Contracts.Interfaces;
using WhoDeenii.DTO.Requests;
using WhoDeenii.DTO.Response;

namespace WhodeeniiCoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttachedDocumentController : ControllerBase
    {
        private readonly IAttachedDocumentService _attachedDocumentService;
        public AttachedDocumentController(IAttachedDocumentService attachedDocumentService)
        {
            _attachedDocumentService = attachedDocumentService;
        }

        [HttpPost]
        [Route("Upload")]
        [Produces(typeof(ApiResponse<string>))]
        public async Task<IActionResult> Upload(AttachDocumentsRequest attach)
        {
            var response = await _attachedDocumentService.UploadFileAsync(attach);
            return Ok(response);
        }

        [HttpGet]
        [Route("byReservationId/{reservationId}")]
        [Produces(typeof(ApiResponse<string>))]
        public async Task<IActionResult> GetAttackedDataByReservationId(string reservationId)
        {
            var response = await _attachedDocumentService.ReservationByReservationIdAsync(reservationId);
            return Ok(response);
        }
    }
}
