using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WhoDeenii.Domain.Contracts.Interfaces;
using WhoDeenii.DTO.Requests;
using WhoDeenii.DTO.Response;
using WhoDeenii.Infrastructure.DataAccess.Entities;
using WhoDeenii.Infrastructure.Repository.Interfaces;

namespace WhoDeenii.Domain.Services.Services
{
    public class SendMessageService : ISendMessageService
    {
        private readonly ISendMessageRepository _whatsAppMessageRepository;
        private readonly IMapper _mapper;
        private readonly ISmsMessageRepository _smsMessageRepository;
        private readonly ILoggerService _loggerService;

        public SendMessageService(ISendMessageRepository whatsAppMessageRepository, IMapper mapper, ISmsMessageRepository smsMessageRepository,ILoggerService loggerService)
        {
            _whatsAppMessageRepository = whatsAppMessageRepository;
            _mapper = mapper;
            _smsMessageRepository = smsMessageRepository;
            _loggerService = loggerService;
        }

        public async Task<ApiResponse<string>> SendMessageAsync(WhatsAppMessageRequest message)
        {
            var response = new ApiResponse<string>();

            try
            {
                var reservationExists = await _whatsAppMessageRepository.CheckReservationIdAsync(message.ReservationId);

                if (!reservationExists)
                {
                    response.IsRequestSuccessful = false;
                    response.Errors = new List<string> { "Reservation ID does not exist in Reservation Table" };
                    return response;
                }

                if (message.SendingMedium.ToLower() == "whatsapp")
                {
                    
                    var WhatsApp = _mapper.Map<WhatsAppMessage>(message);

                    WhatsApp.CreatedDate = DateTime.Now;
                    WhatsApp.SendDate = DateTime.Now.AddHours(3);


                    await _whatsAppMessageRepository.SendMessageAsync(WhatsApp);

                    response.IsRequestSuccessful = true;
                    response.SuccessResponse = "WhatsApp message created successfully.";

                }
                else
                {
                    var sms = _mapper.Map<SmsMessage>(message);

                    sms.CreatedDate = DateTime.Now;
                    sms.SendDate = DateTime.Now.AddHours(3);

                    await _smsMessageRepository.SendMessageAsync(sms);

                    response.IsRequestSuccessful = true;
                    response.SuccessResponse = "SMS message created successfully.";

                }
            }
            catch (Exception ex)
            {
                response.IsRequestSuccessful = false;
                response.ErrorMessage = "An error occurred while sending the message.";

                var logEntry = new LogEntry
                {
                    Level = "Error",
                    Application = "WhoDeenii",
                    MethodInfo = System.Reflection.MethodBase.GetCurrentMethod().Name,
                    Message = ex.Message,
                    Exception = ex.ToString(),
                    Timestamp = DateTime.Now,
                    TransactionId = ex.Message,
                    Context = "Additional context if needed"
                };
                await _loggerService.LogAsync(logEntry);
            }

            return response;
        }
    }
}
