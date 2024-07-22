using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WhoDeenii.Domain.Contracts.Interfaces;
using WhoDeenii.DTO.Requests;
using WhoDeenii.DTO.Response;
using WhoDeenii.Infrastructure.DataAccess.Entities;
using WhoDeenii.Infrastructure.Repository.Interfaces;

namespace WhoDeenii.Domain.Services.Services
{
    public class IDDocumentService : IIDDocumentService
    {
        private readonly IIDDocumentRepository _repository;
        private readonly IMapper _mapper;
        private readonly ImageSettings _imageSettings;
        private readonly ILoggerService _logger;
        
        public IDDocumentService(IIDDocumentRepository repository, IMapper mapper,IOptions<ImageSettings> options,ILoggerService logger)
        {
            _repository = repository;
            _mapper = mapper;
            _imageSettings = options.Value;
            _logger = logger;
            
        }

        public async Task<ApiResponse<string>> AddIDDocumentAsync(IDDocumentRequest idDocumentRequest)
        {
            var response = new ApiResponse<string>();

            var reservationExists = await _repository.CheckReservationIdAsync(idDocumentRequest.ReservationId);
            if (!reservationExists)
            {
                response.IsRequestSuccessful = false;
                response.Errors = new List<string> { "Invalid Reservation Id" };
                return response;
            }

            var existingDocument = await _repository.GetIDDocumentByReservationIdAsync(idDocumentRequest.ReservationId);

            if (existingDocument != null)
            {
                existingDocument.ModifiedDate = DateTime.Now;

                if (idDocumentRequest.ImagePath != null)
                {
                    string timestamp = DateTime.Now.ToString("yyyy MM dd HHmmss");
                    var fileName = $"{timestamp}_{idDocumentRequest.ImagePath.FileName}";
                    var imagePath = Path.Combine(_imageSettings.BasePath, fileName);

                    try
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(imagePath));

                        using (var fileStream = new FileStream(imagePath, FileMode.Create))
                        {
                            await idDocumentRequest.ImagePath.CopyToAsync(fileStream);
                        }

                        existingDocument.ImagePath = imagePath;
                    }
                    catch (Exception ex)
                    {
                        response.IsRequestSuccessful = false;
                        response.Errors = new List<string> { "Failed to save ID document image." };

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

                        await _logger.LogAsync(logEntry);

                        return response;
                    }
                }

                await _repository.UpdateIDDocumentAsync(existingDocument);

                response.IsRequestSuccessful = true;
                response.SuccessResponse = "ID document updated successfully."; 
            }
            else
            {
                var idDocument = _mapper.Map<IDDocument>(idDocumentRequest);
                idDocument.CreatedDate = DateTime.Now;
                idDocument.ModifiedDate = DateTime.Now;

                if (idDocumentRequest.ImagePath != null)
                {
                    string timestamp = DateTime.Now.ToString("yyyy MM dd HHmmss");
                    var fileName = $"{timestamp}_{idDocumentRequest.ImagePath.FileName}";
                    var imagePath = Path.Combine(_imageSettings.BasePath, fileName);

                    try
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(imagePath));

                        using (var fileStream = new FileStream(imagePath, FileMode.Create))
                        {
                            await idDocumentRequest.ImagePath.CopyToAsync(fileStream);
                        }

                        idDocument.ImagePath = imagePath;
                    }
                    catch (Exception ex)
                    {
                        response.IsRequestSuccessful = false;
                        response.Errors = new List<string> { "Failed to save ID document image." };

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

                        await _logger.LogAsync(logEntry);

                        return response;
                    }
                }

                await _repository.AddIDDocumentAsync(idDocument);

                response.IsRequestSuccessful = true;
                response.SuccessResponse = "ID document added successfully.";
            }
            
            return response;
        }


    }

}
