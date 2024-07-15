using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoDeenii.Infrastructure.DataAccess.Entities;

namespace WhoDeenii.Infrastructure.Repository.Interfaces
{
    public interface ISmsMessageRepository
    {
        Task SendMessageAsync(SmsMessage sms);
    }
}
