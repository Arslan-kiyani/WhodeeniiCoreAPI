using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoDeenii.Infrastructure.Repository.Interfaces
{
    public interface IImageRepository
    {
        bool ImageExists(string imagePath);
        Task<byte[]> GetImageBytesAsync(string imagePath);
    }
}
