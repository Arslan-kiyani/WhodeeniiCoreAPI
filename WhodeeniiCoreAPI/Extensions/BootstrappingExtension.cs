using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using WhoDeenii.Domain.Contracts.Interfaces;
using WhoDeenii.Domain.Services.Services;
using WhoDeenii.Infrastructure.Repository.Interfaces;
using Microsoft.Extensions.Options;
using WhodeeniiCoreAPI;

namespace WhoDeenii.API.Extensions
{
    public static class BootstrappingExtension
    {
        public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure ImageSettings
            //services.Configure<ImageSettings>(configuration.GetSection("ImageSettings"));

            // Register dependencies
            services.AddTransient<IReservationService, ReservationService>();
            services.AddTransient<IProfileDetailsService, ProfileDetailsService>();
            services.AddTransient<IRegistrationCardService, RegistrationCardService>();
            services.AddTransient<IIDDocumentService, IDDocumentService>();
            services.AddTransient<ISendMessageService, SendMessageService>();
            //services.AddTransient<ISmsMessageService, SmsMessageService>();
            services.AddTransient<IImageService, ImageService>();
            services.AddTransient<IAttachedDocumentService, AttachedDocumentService>();
            services.AddTransient<ICommentsService, CommentsService>();
            services.AddTransient<IRegisterCapService, RegisterCapService>();
            services.AddTransient<IPhotoService, PhotoService>();
            services.AddHttpClient<PhotoService>();
            services.AddScoped<ILoggerService, LoggerService>();
            //services.AddTransient<ImageSettings>();

            // Register PhotoService with IOptions<ImageSettings>
            //services.AddTransient<IOptions<ImageSettings>>(sp =>
            //{
            //    var configuration = sp.GetRequiredService<IConfiguration>();
            //    var settings = new ImageSettings(); // Initialize with default values if needed
            //    configuration.GetSection("ImageSettings").Bind(settings);
            //    return Options.Create(settings);
            //});
        }
    }
}
