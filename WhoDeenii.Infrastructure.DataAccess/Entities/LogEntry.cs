using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoDeenii.Infrastructure.DataAccess.Entities
{
    public class LogEntry
    {
        public int Id { get; set; }
        public string Level { get; set; }
        public string Application { get; set; }
        public string MethodInfo { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
        public DateTime Timestamp { get; set; }
        public string TransactionId { get; set; }
        public string Context { get; set; }
    }
}
