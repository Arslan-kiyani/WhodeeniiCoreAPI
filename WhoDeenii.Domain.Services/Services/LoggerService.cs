using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoDeenii.Domain.Contracts.Interfaces;
using WhoDeenii.Infrastructure.DataAccess;
using WhoDeenii.Infrastructure.DataAccess.Entities;

namespace WhoDeenii.Domain.Services.Services
{
    public class LoggerService : ILoggerService
    {

        private readonly WhoDeeniiDbContext _dbContext;

        public LoggerService(WhoDeeniiDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task LogAsync(LogEntry logEntry)
        {
            await _dbContext.Logs.AddAsync(logEntry);
            await _dbContext.SaveChangesAsync();
        }
    }
}
