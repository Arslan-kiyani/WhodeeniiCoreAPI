using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoDeenii.Domain.Contracts.Interfaces;
using WhoDeenii.DTO.Response;
using WhoDeenii.Infrastructure.Repository.Interfaces;

namespace WhoDeenii.Domain.Services.Services
{
    public class ImageService : IImageService
    {

        private readonly ILogger<ImageService> _logger;
        private readonly IImageRepository _imageRepository;

        public ImageService(ILogger<ImageService> logger, IImageRepository imageRepository)
        {
            _logger = logger;
            _imageRepository = imageRepository;
        }

        public async Task<ApiResponse<string>> GetImageAsBase64Async(string imagePath)
        {
            var response = new ApiResponse<string>();

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
            return response;
        }

    }
}
