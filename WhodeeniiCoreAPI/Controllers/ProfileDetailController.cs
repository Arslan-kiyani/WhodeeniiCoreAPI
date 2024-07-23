using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WhoDeenii.Domain.Contracts.Interfaces;
using WhoDeenii.DTO.Requests;
using WhoDeenii.DTO.Response;


namespace WhodeeniiCoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileDetailController : ControllerBase
    {
        private readonly IProfileDetailsService _profileDetailsService;

        public ProfileDetailController(IProfileDetailsService profileDetailsService)
        {
            _profileDetailsService = profileDetailsService;
        }

        [HttpPost]
        [Route("api/AddProfileDetail")]
        [Produces(typeof(ApiResponse<string>))]
        public async Task<IActionResult> AddProfileDetail(ProfileDetailRequest request)
        {
            var response = await _profileDetailsService.AddProfileDetailsAsync(request);
            return Ok(response);
        }

        [HttpGet]
        [Route("byReservationId/{reservationId}")]
        [Produces(typeof(ApiResponse<string>))]
        public async Task<IActionResult> GetProfileDetailsByReservationId(string reservationId)
        {
            var response = await _profileDetailsService.GetProfileDetailsByReservationIdAsync(reservationId);
            return Ok(response);
        }
    }

}
