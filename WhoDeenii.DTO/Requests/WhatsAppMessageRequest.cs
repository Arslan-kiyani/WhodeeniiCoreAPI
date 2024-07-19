using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoDeenii.DTO.Requests
{
    public class WhatsAppMessageRequest
    {
        public string? ReservationId { get; set; }
        public string? MobileNumber { get; set; }
       // public bool IsSent { get; set; }
        public string? SendingMedium { get; set;}
         public bool IsProfileDetailsSent {  get; set; }
        public bool IsRegistrationCardSent { get; set; }
        public bool IsIDDocumentSent { get; set; }

    }
}
