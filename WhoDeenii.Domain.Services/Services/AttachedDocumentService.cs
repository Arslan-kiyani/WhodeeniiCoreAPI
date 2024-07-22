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

    public AttachedDocumentService(IAttachDocumentsRepository repository, IOptions<ImageSettings> options)
    {
        _repository = repository;
        _BasePath2 = options.Value;
    }

    public async Task<ApiResponse<string>> DeleteAttachedDocumentAsync(string ReservationId)
    {
        var response = new ApiResponse<string>();

        try
        {
            var isDeleted = await _repository.DeleteAttachedDocumentAsync(ReservationId);

            if (isDeleted)
            {
                response.IsRequestSuccessful = true;
                response.SuccessResponse = "Document deleted successfully.";
            }
            else
            {
                response.IsRequestSuccessful = false;
                response.Errors = new List<string> { "Document not found." };
            }
        }
        catch (Exception ex)
        {
            response.IsRequestSuccessful = false;
            response.Errors.Add(ex.Message);
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
                response.Errors = new List<string> { "invalid Reservation Id" };
                return response;
            }
            if (attach.file == null || attach.file.Length == 0)
            {
                 response.IsRequestSuccessful = false;
                 response.ErrorMessage = "No file uploaded.";
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

            var fileName = Guid.NewGuid().ToString() + extension;
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await attach.file.CopyToAsync(fileStream);
            }

            string dateTimeString = "2024-07-23 14:00:00"; 
            DateTime uploadDate = DateTime.ParseExact(dateTimeString, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

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
        }

        return response;
    }
}
