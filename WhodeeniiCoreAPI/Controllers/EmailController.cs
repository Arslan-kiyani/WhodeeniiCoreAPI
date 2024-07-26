using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WhoDeenii.Infrastructure.DataAccess.Entities;

namespace WhodeeniiCoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly EmailReaderService _emailReaderService;

        public EmailController(EmailReaderService emailReaderService)
        {
            _emailReaderService = emailReaderService;
        }


        [HttpGet]
        [Route("Get-Emails")]
        public async Task<IActionResult> GetEmails()
        {
            try
            {

                List<MimeMessage> emails = await _emailReaderService.ReadEmailsAsync();

                var emailDtoList = new List<EmailDto>();

                foreach (var email in emails)
                {
                    emailDtoList.Add(new EmailDto
                    {
                        Subject = email.Subject,
                        From = email.From.ToString(),
                        Date = email.Date.DateTime,
                        Body = email.HtmlBody ?? "N/A",
                    });
                }

                return Ok(emailDtoList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
            
        }

    }
}
