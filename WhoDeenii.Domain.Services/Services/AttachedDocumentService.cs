using Microsoft.AspNetCore.Http;
using System.Linq;
using WhoDeenii.Domain.Contracts.Interfaces;
using WhoDeenii.DTO.Requests;
using WhoDeenii.DTO.Response;
using WhoDeenii.Infrastructure.DataAccess.Entities;
using WhoDeenii.Infrastructure.Repository.Interfaces;

public class AttachedDocumentService : IAttachedDocumentService
{
    private readonly IAttachDocumentsRepository _repository;
    private readonly string _localDrivePath;

    public AttachedDocumentService(IAttachDocumentsRepository repository)
    {
        _repository = repository;
        _localDrivePath = @"C:\Users\laptop wala\Documents\uploadsFile"; 
    }

    public async Task<ApiResponse<string>> UploadFileAsync(IFormFile file)
    {
        var response = new ApiResponse<string>();

        try
        {
            if (file == null || file.Length == 0)
            {
                 response.IsRequestSuccessful = false;
                 response.ErrorMessage = "No file uploaded.";
                 return response;
            }
            var allowedExtensions = new[] { ".pdf", ".png", ".jpg", ".jpeg" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(extension))
            {
                response.IsRequestSuccessful = false;
                response.ErrorMessage = "Invalid file type.";
                return response;
            }

            var uploadsFolderPath = Path.Combine(_localDrivePath);

            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }

            var fileName = Guid.NewGuid().ToString() + extension;
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            
            //var uploadedFileType = Path.GetExtension(filePath)?.ToLowerInvariant();
            //var requestedFileType = "." + request.DocumentType.ToLowerInvariant();

            //if (uploadedFileType != requestedFileType)
            //{
            //    response.IsRequestSuccessful = false;
            //    response.ErrorMessage = "Uploaded file type does not match the requested document type.";
            //    return response;
            //}

            var document = new AttachDocuments
            {
                UploadDate = DateTime.Now,
                FilePath = filePath
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
