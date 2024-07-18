

using Microsoft.AspNetCore.Http;

namespace WhoDeenii.DTO.Requests
{
    public class IDDocumentRequest
    {
        public  IFormFile ImagePath { get; set; }
        public  string ReservationId { get; set; }

    }
}
