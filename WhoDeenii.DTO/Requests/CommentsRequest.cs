using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoDeenii.DTO.Requests
{
    public class CommentsRequest
    {
        public string? CreatedBy { get; set; }
        public string? CommentText { get; set; }
        public string? ReservationId { get; set; }
        
    }
}
