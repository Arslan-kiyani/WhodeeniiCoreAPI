using Microsoft.AspNetCore.Http;

namespace WhoDeenii.DTO.Requests
{
    public class RegistrationCardRequest
    {
        public IFormFile Imagepath { get; set; }
        public bool IsDeleted { get; set; }
        public string ReservationId { get; set; }
    }
}
