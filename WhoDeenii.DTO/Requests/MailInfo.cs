using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoDeenii.DTO.Requests
{
    public class MailInfo
    {
        public string MessageId { get; set; }
        public string Subject { get; set; }
        public bool IsRead { get; set; }
    }
}
