using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WhoDeenii.Domain.Contracts.Interfaces;
using WhoDeenii.DTO.Requests;
using WhoDeenii.DTO.Response;

namespace WhodeeniiCoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentsService _commentsService;

        public CommentsController(ICommentsService commentsService)
        {
            _commentsService = commentsService;
        }

        [HttpPost]
        [Route("AddComment")]
        [Produces(typeof(ApiResponse<string>))]
        public async Task<IActionResult> AddComment(CommentsRequest request)
        {
            var response = await _commentsService.AddCommentAsync(request);
            return Ok(response);
        }

        [HttpGet]
        [Route("{reservationId}")]
        [Produces(typeof(ApiResponse<string>))]
        public async Task<IActionResult> GetCommentsByReservationId(string reservationId)
        {
            var response = await _commentsService.GetCommentsByReservationIdAsync(reservationId);
            return Ok(response);
        }

    }
}
