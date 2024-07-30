using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoDeenii.Domain.Contracts.Interfaces
{
    public interface IEmailReaderService
    {
        Task<List<MimeMessage>> ReadEmailsAsync();
    }
}
