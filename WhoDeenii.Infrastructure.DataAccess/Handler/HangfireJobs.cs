using Hangfire;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoDeenii.Infrastructure.DataAccess.Handler
{
  
    public class HangfireJobs
    {
        private readonly HangfireConfig _hangfireConfig;
        private readonly HangfireConfigString _hangfireConfigString;
        public HangfireJobs(IOptions<HangfireConfig> hangfireConfig, IOptions<HangfireConfigString> hangfireConfigString)
        {

            _hangfireConfig = hangfireConfig.Value;
            _hangfireConfigString = hangfireConfigString.Value;
        }
        public void CreateJob()
        {
           // RecurringJob.AddOrUpdate<EmailReaderService>(_hangfireConfigString.EmailJob, t => t.TransferData(), _hangfireConfig.EmailJob);

#if DEBUG
#else

            RecurringJob.AddOrUpdate<OnlineCheckInLogsJob>(_hangfireConfigString.OnlineCheckInLogsJob, t => t.TransferData(), _hangfireConfig.OnlineCheckInLogsJobCron);
            RecurringJob.AddOrUpdate<AuditJob>(_hangfireConfigString.AuditDataTransferJob, t => t.TransferData(), _hangfireConfig.AuditDataTransferJobCron);
            RecurringJob.AddOrUpdate<WhatsAppJob>(_hangfireConfigString.WhatsAppDataTransferJob, t => t.TransferData(), _hangfireConfig.WhatsAppDataTransferJobCron);
            RecurringJob.AddOrUpdate<SmsMessageLogJob>(_hangfireConfigString.SmsMessageLogJob, t => t.TransferData(), _hangfireConfig.SmsMessageLogJobCron);
            RecurringJob.AddOrUpdate<Inventory_ArchiveJob>(_hangfireConfigString.Inventory_ArchiveJob, t => t.TransferData(), _hangfireConfig.Inventory_ArchiveJobCron);
            RecurringJob.AddOrUpdate<Logjob>(_hangfireConfigString.LogJob, t => t.TransferData(), _hangfireConfig.LogJobCron);

#endif
        }
    }
}
