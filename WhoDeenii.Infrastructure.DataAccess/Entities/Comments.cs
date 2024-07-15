using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoDeenii.Infrastructure.DataAccess.Entities
{
    public class Comments
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public string CommentText { get; set; }
        public string ReservationId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
