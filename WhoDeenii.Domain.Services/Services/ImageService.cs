using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoDeenii.Domain.Contracts.Interfaces;
using WhoDeenii.DTO.Response;
using WhoDeenii.Infrastructure.DataAccess.Entities;
using WhoDeenii.Infrastructure.Repository.Interfaces;

namespace WhoDeenii.Domain.Services.Services
{
    public class ImageService : IImageService
    {

        private readonly IImageRepository _imageRepository;
        private readonly ILoggerService _logger;
        public ImageService(IImageRepository imageRepository, ILoggerService logger)
        {
            _imageRepository = imageRepository;
            _logger = logger;
        }

        public async Task<ApiResponse<string>> GetImageAsBase64Async(string imagePath)
        {
            var response = new ApiResponse<string>();

            try
            {
                if (_imageRepository.ImageExists(imagePath))
                {
                    var imageBytes = await _imageRepository.GetImageBytesAsync(imagePath);
                    var base64String = Convert.ToBase64String(imageBytes);

                    response.IsRequestSuccessful = true;
                    response.SuccessResponse = base64String;
                }
                else
                {
                    response.IsRequestSuccessful = false;
                    response.Errors = new List<string> { "Image not found." };
                }
            }catch (Exception ex)
            {
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
}
