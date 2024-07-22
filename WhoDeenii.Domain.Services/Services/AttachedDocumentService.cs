using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Linq;
using WhoDeenii.Domain.Contracts.Interfaces;
using WhoDeenii.Domain.Services.Services;
using WhoDeenii.DTO.Requests;
using WhoDeenii.DTO.Response;
using WhoDeenii.Infrastructure.DataAccess.Entities;
using WhoDeenii.Infrastructure.Repository.Interfaces;

public class AttachedDocumentService : IAttachedDocumentService
{
    private readonly IAttachDocumentsRepository _repository;
    private readonly ImageSettings _BasePath2;
    private readonly ILoggerService _logger;

    public AttachedDocumentService(IAttachDocumentsRepository repository, IOptions<ImageSettings> options,ILoggerService logger)
    {
        _repository = repository;
        _BasePath2 = options.Value;
        _logger = logger;   
    }

    public async Task<ApiResponse<string>> DeleteAttachedDocumentAsync(int id)
    {
        var response = new ApiResponse<string>();

        try
        {
            var isDeleted = await _repository.DeleteAttachedDocumentAsync(id);

            if (isDeleted)
            {
                response.IsRequestSuccessful = true;
                response.SuccessResponse = "Document deleted successfully.";
            }
            else
            {
                response.IsRequestSuccessful = false;
                response.Errors = new List<string> { "Document not found." };
                return response;
            }
        }
        catch (Exception ex)
        {
            response.IsRequestSuccessful = false;
            response.SuccessResponse = ex.Message;
            response.Errors = new List<string> { { $"Something went wrong Error: " } };

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
        }

        return response;
    }

    public async Task<ApiResponse<List<AttachDocuments>>> ReservationByReservationIdAsync(string reservationId)
    {
        var response = new ApiResponse<List<AttachDocuments>>();
        try
        {
            var attachDocumentsDetails = await _repository.GetByReservationIdAsync(reservationId);
            if (attachDocumentsDetails != null && attachDocumentsDetails.Any())
            {
                response.IsRequestSuccessful = true;
                response.SuccessResponse = attachDocumentsDetails.Select(doc => new AttachDocuments
                {
                    DocumentType = doc.DocumentType,
                    UploadDate = doc.UploadDate,
                    FilePath = doc.FilePath,
                    ReservationId = reservationId
                }).ToList();
            }
            else
            {
                response.IsRequestSuccessful = false;
                response.ErrorMessage = "Reservation details not found.";
            }
        }
        catch (Exception ex)
        {
            response.IsRequestSuccessful = false;
            response.Errors = new List<string> { ex.Message };

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
        }

        return response;
    }


    public async Task<ApiResponse<string>> UploadFileAsync(AttachDocumentsRequest attach)
    {
        var response = new ApiResponse<string>();

        try
        {

            var reservationExists = await _repository.CheckReservationIdAsync(attach.ReservationId);
            if (!reservationExists)
            {
                response.IsRequestSuccessful = false;
                response.Errors = new List<string> { "Invalid Reservation Id" };
                return response;
            }
            if (attach.file == null || attach.file.Length == 0)
            {
                 response.IsRequestSuccessful = false;
                 response.ErrorMessage = "No file Found.";
                 return response;
            }
            var allowedExtensions = new[] { ".pdf", ".png", ".jpg", ".jpeg" };
            var extension = Path.GetExtension(attach.file.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(extension))
            {
                response.IsRequestSuccessful = false;
                response.ErrorMessage = "Invalid file type.";
                return response;
            }

            var uploadsFolderPath = Path.Combine(_BasePath2.Basepath2);

            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }

            string timestamp = DateTime.Now.ToString("yyyy MM dd HHmmss");
            var fileName = $"{timestamp}{extension}";
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await attach.file.CopyToAsync(fileStream);
            }

            var document = new AttachDocuments
            {
                UploadDate = DateTime.Now,
                FilePath = filePath,
                DocumentType = attach.DocumentType,
                ReservationId = attach.ReservationId,   
            };

            await _repository.AddDocumentAsync(document);

            response.IsRequestSuccessful = true;
            response.SuccessResponse = "File uploaded successfully.";
        }
        catch (Exception ex)
        {
            response.IsRequestSuccessful = false;
            response.SuccessResponse = ex.Message;
            response.Errors = new List<string> { { $"Something went wrong Error: " } };

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
        }

        return response;
    }
}
