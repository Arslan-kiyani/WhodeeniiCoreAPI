using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WhoDeenii.Domain.Contracts.Interfaces;
using WhoDeenii.DTO.Response;
using WhoDeenii.Infrastructure.DataAccess;
using WhoDeenii.Infrastructure.DataAccess.Entities;

namespace WhodeeniiCoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetReservationDetailsController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public GetReservationDetailsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        [Route("{reservationId}")]
        [Produces(typeof(ApiResponse<string>))]
        public async Task<ActionResult<ApiResponse<GetReservationDetails>>> GetReservationDetails(string reservationId)
        {
            var response = await _reservationService.GetReservationDetailsByReservationIdAsync(reservationId);
            return Ok(response);
               
        }
    }
}
