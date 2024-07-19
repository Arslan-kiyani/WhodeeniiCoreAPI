using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoDeenii.Infrastructure.DataAccess.Entities
{
    public class WhatsAppMessage
    {
        public int Id { get; set; } 
        public string? ReservationId { get; set; } 
        public string? MobileNumber { get; set; } 
        public DateTime CreatedDate { get; set; } 
        public bool IsSent { get; set; } 
        public DateTime? SendDate { get; set; }
        public bool IsProfileDetailsSent { get; set; }
        public bool IsRegistrationCardSent { get; set; }
        public bool IsIDDocumentSent { get; set; }
    }
}
