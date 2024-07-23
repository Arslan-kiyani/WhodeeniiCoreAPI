using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.IO;
using WhoDeenii.Domain.Contracts.Interfaces;
using WhoDeenii.DTO.Requests;
using WhoDeenii.DTO.Response;
using WhoDeenii.Infrastructure.DataAccess.Entities;
using WhoDeenii.Infrastructure.Repository.Interfaces;


namespace WhoDeenii.Domain.Services.Services
{
    public class RegistrationCardService : IRegistrationCardService
    {
        private readonly IRegistrationCardRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILoggerService _loggerService;
        private readonly ImageSettings _imageSettings;
        public RegistrationCardService(IRegistrationCardRepository repository, IMapper mapper, ILoggerService loggerService,IOptions<ImageSettings>  imageSettings)
        {
            _repository = repository;
            _mapper = mapper;
            _loggerService = loggerService;
            _imageSettings = imageSettings.Value;
        }

        public async Task<ApiResponse<string>> AddRegistrationCardAsync(RegistrationCardRequest request)
        {
            var response = new ApiResponse<string>();

            
            var reservationExists = await _repository.CheckReservationIdAsync(request.ReservationId);
            if (!reservationExists)
            {
                response.IsRequestSuccessful = false;
                response.Errors = new List<string> { "Reservation ID does not exist in Reservation Table" };
                return response;
            }

            var existingRegistrationCard = await _repository.GetRegistrationCardByReservationIdAsync(request.ReservationId);

            if (existingRegistrationCard != null)
            {
                existingRegistrationCard.ModifiedDate = DateTime.Now;
                if (request.Imagepath != null)
                {
                    string timestamp = DateTime.Now.ToString("yyyy MM dd HHmmss");
                    var fileName = $"{timestamp}_{request.Imagepath.FileName}";
                    var imagePath = Path.Combine(_imageSettings.BasePath, fileName);

                    try
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(imagePath));

                        using (var fileStream = new FileStream(imagePath, FileMode.Create))
                        {
                            await request.Imagepath.CopyToAsync(fileStream);
                        }

                        existingRegistrationCard.Imagepath = imagePath;
                    }
                    catch (Exception ex)
                    {
                        
                        response.IsRequestSuccessful = false;
                        response.Errors = new List<string> { "Error saving registration card image" };

                        var logEntry = new LogEntry
                        {
                            Level = "Error",
                            Application = "WhoDeenii",
                            MethodInfo = "SomeService.DoSomethingAsync",
                            Message = ex.Message,
                            Exception = ex.ToString(),
                            Timestamp = DateTime.Now,
                            TransactionId = ex.Message,
                            Context = "Additional context if needed"
                        };
                        await _loggerService.LogAsync(logEntry);

                        return response;
                    }
                }

                await _repository.UpdateRegistrationCardAsync(existingRegistrationCard);

                response.IsRequestSuccessful = true;
                response.SuccessResponse = "Registration card updated successfully.";
                return response;
            }
            else
            {
                    
                var registrationCard = _mapper.Map<RegistrationCard>(request);
                registrationCard.CreatedDate = DateTime.Now;
                registrationCard.ModifiedDate = DateTime.Now;

                if (request.Imagepath != null)
                {
                    var fileName = $"{Guid.NewGuid()}_{request.Imagepath.FileName}";
                    var imagePath = Path.Combine("C:\\Users\\laptop wala\\Documents\\Images", fileName);

                    try
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(imagePath));

                        using (var fileStream = new FileStream(imagePath, FileMode.Create))
                        {
                            await request.Imagepath.CopyToAsync(fileStream);
                        }

                        registrationCard.Imagepath = imagePath;
                    }
                    catch (Exception ex)
                    {
                        
                        response.IsRequestSuccessful = false;
                        response.Errors = new List<string> { "Error saving registration card image" };

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

                        return response;
                    }
                }

                await _repository.AddRegistrationCardAsync(registrationCard);

                response.IsRequestSuccessful = true;
                response.SuccessResponse = "Registration card added successfully.";
                return response;
            }  
        }
    }
}
