using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoDeenii.Infrastructure.DataAccess;
using WhoDeenii.Infrastructure.DataAccess.Entities;
using WhoDeenii.Infrastructure.Repository.Interfaces;

namespace WhoDeenii.Infrastructure.Repository.Respositories
{
    public class SmsMessageRepository : ISmsMessageRepository
    {
        private readonly WhoDeeniiDbContext _whoDeeniiDbContext;
        public SmsMessageRepository(WhoDeeniiDbContext whoDeeniiDbContext)
        {
            _whoDeeniiDbContext = whoDeeniiDbContext;
        }

        public async Task SendMessageAsync(SmsMessage sms)
        {
            await _whoDeeniiDbContext.AddAsync(sms);
            await _whoDeeniiDbContext.SaveChangesAsync();
            
        }
    }
}
