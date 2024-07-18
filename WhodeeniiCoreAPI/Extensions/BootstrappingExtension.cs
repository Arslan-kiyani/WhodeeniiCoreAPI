using Microsoft.Extensions.DependencyInjection;
using WhoDeenii.Domain.Contracts.Interfaces;
using WhoDeenii.Domain.Services.Services;
using WhodeeniiCoreAPI;


namespace WhoDeenii.API.Extensions
{
    public static class BootstrappingExtension
    {
        public static void RegisterDependencies(this IServiceCollection services)
        {
            services.AddTransient<IReservationService, ReservationService>();
            services.AddTransient<IProfileDetailsService, ProfileDetailsService>();
            services.AddTransient<IRegistrationCardService, RegistrationCardService>();
            services.AddTransient<IIDDocumentService, IDDocumentService>();
            services.AddTransient<ISendMessageService, SendMessageService>();
            //services.AddTransient<ISmsMessageService, SmsMessageService>();
            services.AddTransient<IImageService, ImageService>();
            services.AddTransient<IAttachedDocumentService, AttachedDocumentService>();
            services.AddTransient<ICommentsService, CommentsService>();
            services.AddScoped<IPhotoService, PhotoService>();
        }
    }
}
