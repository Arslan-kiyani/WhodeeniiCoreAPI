using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoDeenii.DTO.Requests
{
    public class CapRequest
    {
        public string ReservationId { get; set; }
        public string ImageBytes { get; set; }
    }
}
