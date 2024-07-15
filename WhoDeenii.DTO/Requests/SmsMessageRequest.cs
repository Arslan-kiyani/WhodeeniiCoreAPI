using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoDeenii.DTO.Requests
{
    public class SmsMessageRequest
    {
        public string? MobileNumber { get; set; }
        public bool IsSent { get; set; }
        public string? ReservationId { get; set; }
    }
}
