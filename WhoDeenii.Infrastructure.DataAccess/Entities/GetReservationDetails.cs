using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoDeenii.Infrastructure.DataAccess.Entities
{


    public class GetReservationDetailsResponse
    {
        public GetReservationDetails? ReservationDetails { get; set; }
        public string MissingTablesMessage { get; set; }
    }


    public class GetReservationDetails
    {
        public int Id { get; set; }
        public string? ReservationId { get; set; }
         //public DateTime? CreatedDateRegistrationCards { get; set; }
        public string ModifiedDateRegistrationCards { get; set; }
        //public DateTime? CreatedDateIDDocuments { get; set; }
        public string? ModifiedDateIDDocuments { get; set; }

        public string RegistrationCardsimagePath { get; set; }
        public string IDDocumentsimagePath { get; set; }
        public string? ProfileCreatedDate { get; set; }
        public bool IsProfileUpdated { get; set; }

    }

   
}
