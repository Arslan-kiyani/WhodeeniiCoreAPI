using Microsoft.Extensions.Options;
using OpenCvSharp;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using WhoDeenii.Domain.Contracts.Interfaces;
using WhoDeenii.DTO.Requests;
using WhoDeenii.DTO.Response;
using WhoDeenii.Infrastructure.DataAccess.Entities;
using WhoDeenii.Infrastructure.Repository.Interfaces;
using WhoDeenii.Infrastructure.Repository.Respositories;

namespace WhoDeenii.Domain.Services.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IPhotoRepository _photoRepository;
        //private readonly string _imageBasePath = @"C:\Users\laptop wala\Documents\Images"; 
        private readonly ImageSettings _imageBasePath;
        public PhotoService(IPhotoRepository photoRepository, IOptions<ImageSettings> options)
        {
            _photoRepository = photoRepository;
            _imageBasePath = options.Value;
        }

        public async Task<ApiResponse<string>> SaveImageAsync(CapRequest request)
        {

            var response = new ApiResponse<string>();

            try
            {
                byte[] imageData = GetImageDataFromRequest(request);

                string timestamp = DateTime.Now.ToString("yyyy MM dd HHmmss");
                string fileName = $"{timestamp}.jpg";
                string filePath = Path.Combine(_imageBasePath.BasePath, fileName);
                
                await SaveFileAsync(filePath, imageData);

                var existingImageData = await _photoRepository.GetPhotoDocumentAsync(request.ReservationId);

                if (existingImageData != null)
                {
                    existingImageData.ModifiedDate = DateTime.Now;
                    existingImageData.ImagePath = filePath;

                    await _photoRepository.UpdatePhotoDocumentAsync(existingImageData);
                }
                else
                {
                    var img = new IDDocument
                    {
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        ImagePath = filePath,
                        ReservationId = request.ReservationId,
                    };

                    await _photoRepository.AddPhotoDocumentAsync(img);
                }

                response.IsRequestSuccessful = true;
                response.SuccessResponse = "IDDocument added or updated successfully.";
            }
            catch (Exception ex)
            {
                response.IsRequestSuccessful = false;
                response.Errors = new List<string> { ex.Message };
                return response;
            }

            return response;
        }
        
        private async Task SaveFileAsync(string filePath, byte[] fileData)
        {
            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            }

            using (var fileStream = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write))
            {
                await fileStream.WriteAsync(fileData, 0, fileData.Length);
                await fileStream.FlushAsync();
            }
        }

        private byte[] GetImageDataFromRequest(CapRequest request)
        {
            if (string.IsNullOrEmpty(request.ImageBytes))
            {
                throw new ArgumentException("Missing image data in request.");
            }

            string base64Data = request.ImageBytes;
            byte[] imageData = Convert.FromBase64String(base64Data);
            return imageData;
        }


    }
}
