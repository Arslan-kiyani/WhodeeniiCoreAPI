using FluentAssertions.Common;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> Upload(AttachDocumentsRequest request)
        {
            var response = await _attachedDocumentService.UploadFileAsync(request);
            return Ok(response);
        }

        [HttpGet]
        [Authorize]
        [Route("Get-byReservationId/{reservationId}")]
        [Produces(typeof(ApiResponse<string>))]
        public async Task<IActionResult> GetAttackedDataByReservationId(string reservationId)
        {
            var response = await _attachedDocumentService.ReservationByReservationIdAsync(reservationId);
            return Ok(response);
        }

        [HttpDelete]
        [Route("{id}")]
        [Produces(typeof(ApiResponse<string>))]
        public async Task<IActionResult> DeleteAttachedDocument(int id)
        {
            var response = await _attachedDocumentService.DeleteAttachedDocumentAsync(id);
            return Ok(response);
        }
    }
}
