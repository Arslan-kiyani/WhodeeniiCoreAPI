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

        public ReservationController(IReservationService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("api/AddReservation")]
        [Produces(typeof(ApiResponse<string>))]
        public async Task<IActionResult> AddReservation(ReservationRequest request)
        {
            var response = await _service.AddReservationAsync(request);
             return Ok(response);
        }

        [HttpGet]
        [Route("byReservationId/{reservationId}")]
        [Produces(typeof(ApiResponse<string>))]
        public async Task<IActionResult> GetProfileDetailsByReservationId(string reservationId)
        {
            var response = await _service.ReservationByReservationIdAsync(reservationId);
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
