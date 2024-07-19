using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoDeenii.Domain.Contracts.Interfaces;
using WhoDeenii.DTO.Requests;
using WhoDeenii.DTO.Response;
using WhoDeenii.Infrastructure.DataAccess.Entities;
using WhoDeenii.Infrastructure.Repository.Interfaces;
using WhoDeenii.Infrastructure.Repository.Respositories;

namespace WhoDeenii.Domain.Services.Services
{
    public class RegisterCapService : IRegisterCapService
    {

        private readonly IRegisterCapRepository _registerCap;
        private readonly string _imageBasePath = @"C:\Users\laptop wala\Documents\Images";
        public RegisterCapService(IRegisterCapRepository registerCap)
        {
            _registerCap = registerCap;
        }
        public async Task<ApiResponse<string>> SaveImageAsync(CapRequest request)
        {
            var response = new ApiResponse<string>();

            try
            {
                string timestamp = DateTime.Now.ToString("yyyy MM dd HHmmss");
                string fileName = $"{timestamp}.jpg";
                string filePath = Path.Combine(_imageBasePath, fileName);

                var existingImageData = await _registerCap.GetPhotoDocumentAsync(request.ReservationId);

                if (existingImageData != null)
                {
                    existingImageData.ModifiedDate = DateTime.Now;
                    existingImageData.Imagepath = filePath;

                    await _registerCap.UpdatePhotoDocumentAsync(existingImageData);
                }
                else
                {
                    var imageData = new RegistrationCard
                    {
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        Imagepath = filePath,
                        ReservationId = request.ReservationId,
                    };

                    await _registerCap.AddPhotoDocumentAsync(imageData);
                }

                response.IsRequestSuccessful = true;
                response.SuccessResponse = "RegisterationCard added or updated successfully.";
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
