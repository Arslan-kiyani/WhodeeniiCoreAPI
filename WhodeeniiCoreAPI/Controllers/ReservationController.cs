using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WhoDeenii.Domain.Contracts.Interfaces;
using WhoDeenii.DTO.Requests;
using WhoDeenii.DTO.Response;


namespace WhodeeniiCoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _service;
        private readonly ILogger<ReservationController> _logger;

        public ReservationController(IReservationService service, ILogger<ReservationController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        [Route("api/AddReservation")]
        [Produces(typeof(ApiResponse<string>))]
        public async Task<IActionResult> AddReservation([FromForm] ReservationRequest reservationRequest)
        {
            var response = await _service.AddReservationAsync(reservationRequest);
             return Ok(response);
        }

        //[HttpGet]
        //[Route("{id}")]
        //[Produces(typeof(ApiResponse<string>))]
        //public async Task<IActionResult> GetreservationId(int id)
        //{
        //    var response = await _service.GetReservationByIdAsync(id);
        //    return Ok(response);
        //}
    }

}
