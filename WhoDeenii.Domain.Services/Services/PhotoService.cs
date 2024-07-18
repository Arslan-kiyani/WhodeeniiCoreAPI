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
        private readonly string _imageBasePath = @"C:\Users\laptop wala\Documents\Images"; 
        public PhotoService(IPhotoRepository photoRepository)
        {
            _photoRepository = photoRepository;
        }

        public async Task<ApiResponse<string>> SaveImageAsync(CapRequest request)
        {

            var response = new ApiResponse<string>();

            try
            {
                //byte[] imageBytes = Convert.FromBase64String(request.ImageBytes);
                string fileName = $"{Guid.NewGuid().ToString()}.jpg";
                string filePath = Path.Combine(_imageBasePath, fileName);

                //await File.WriteAllBytesAsync(filePath, imageBytes);

                var existingImageData = await _photoRepository.GetPhotoDocumentAsync(request.ReservationId);

                if (existingImageData != null)
                {
                    existingImageData.ModifiedDate = DateTime.Now;
                    existingImageData.ImagePath = filePath;

                    await _photoRepository.UpdatePhotoDocumentAsync(existingImageData);
                }

                else
                {
                    var imageData = new IDDocument
                    {
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        ImagePath = filePath,
                        ReservationId = request.ReservationId,
                    };

                    await _photoRepository.AddPhotoDocumentAsync(imageData);
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
       

    }
}
