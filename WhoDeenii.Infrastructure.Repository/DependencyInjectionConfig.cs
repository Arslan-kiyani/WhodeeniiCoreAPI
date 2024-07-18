using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoDeenii.Infrastructure.DataAccess;
using WhoDeenii.Infrastructure.Repository.Interfaces;
using WhoDeenii.Infrastructure.Repository.Respositories;

namespace WhoDeenii.Infrastructure.Repository
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterRepository(IServiceCollection services)
        {
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddTransient<IProfileDetailsRepository, ProfileDetailsRepository>();
            services.AddTransient<IRegistrationCardRepository, RegistrationCardRepository>();
            services.AddTransient<IIDDocumentRepository, IDDocumentRepository>();
            services.AddTransient<ISendMessageRepository, SendMessageRepository>();
            services.AddTransient<IImageRepository, ImageRepository>();
            services.AddTransient<ISmsMessageRepository, SmsMessageRepository>();
            services.AddTransient<IAttachDocumentsRepository, AttachDocumentsRepository>();
            services.AddTransient<ICommentsRepository, CommentsRepository>();
            services.AddTransient<IPhotoRepository, PhotoRepository>();
            
        }
    }
}
