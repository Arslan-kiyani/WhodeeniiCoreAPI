using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoDeenii.Infrastructure.DataAccess.Entities
{
    public class AttachDocuments
    {
        public int Id { get; set; }
        public DateTime UploadDate { get; set; }
        public string FilePath { get; set; }
        public string DocumentType { get; set; }
        public bool IsDeleted { get; set; }
        public string? ReservationId { get; set; }

    }
}
