using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoDeenii.DTO.Requests
{
    public class AttachDocumentsRequest
    {
        public string DocumentType { get; set; }
        public IFormFile file { get; set; }
        public string? ReservationId { get; set; }
    }
}
