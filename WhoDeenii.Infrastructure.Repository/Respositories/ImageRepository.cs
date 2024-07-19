using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoDeenii.DTO.Requests;
using WhoDeenii.Infrastructure.Repository.Interfaces;

namespace WhoDeenii.Infrastructure.Repository.Respositories
{
    public class ImageRepository : IImageRepository
    {
        public bool ImageExists(string imagePath)
        {
            return File.Exists(imagePath);
        }

        public async Task<byte[]> GetImageBytesAsync(string imagePath)
        {
            return await File.ReadAllBytesAsync(imagePath);
        }

    }
}
