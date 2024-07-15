using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoDeenii.DTO.Requests
{
    public class GetReservationDetailsRequest
    {
        public string? ReservationId { get; set; }
        //public DateTime? CreatedDateRegistrationCards { get; set; }
        public DateTime? ModifiedDateRegistrationCards { get; set; }
        //public DateTime? CreatedDateIDDocuments { get; set; }
        public DateTime? ModifiedDateIDDocuments { get; set; }
        public string? RegistrationCardsimagePath { get; set; }
        public string? IDDocumentsimagePath { get; set; }
        public DateTime? ProfileCreatedDate { get; set; }
        public bool IsProfileUpdated { get; set; }
    }
}
