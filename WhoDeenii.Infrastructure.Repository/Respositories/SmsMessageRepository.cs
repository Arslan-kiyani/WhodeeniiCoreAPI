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
        private readonly ILogger<SmsMessageRepository> _logger;
        public SmsMessageRepository(WhoDeeniiDbContext whoDeeniiDbContext, ILogger<SmsMessageRepository> logger)
        {
            _whoDeeniiDbContext = whoDeeniiDbContext;
            _logger = logger;
        }
        public async Task SendMessageAsync(SmsMessage sms)
        {
            try
            {
                await _whoDeeniiDbContext.AddAsync(sms);
                await _whoDeeniiDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding reservation details");

            }
        }
    }
}
