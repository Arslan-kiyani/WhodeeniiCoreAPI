using Microsoft.EntityFrameworkCore;
using WhoDeenii.API.Extensions;
using WhoDeenii.Domain.Contracts.Interfaces;
using WhoDeenii.Infrastructure.DataAccess;
using WhoDeenii.Infrastructure.Repository;
using WhoDeenii.Infrastructure.Repository.Interfaces;
using WhoDeenii.Infrastructure.Repository.Mappers;
using WhoDeenii.Infrastructure.Repository.Respositories;

namespace WhodeeniiCoreAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure logging
            builder.Logging.ClearProviders(); 
            builder.Logging.AddConsole();   

            // Add services to the container.
            builder.Services.AddDbContext<WhoDeeniiDbContext>(options =>
                   options.UseSqlServer(builder.Configuration.GetConnectionString("WhoDeenii")));

            builder.Services.AddControllers();

            // Add AutoMapper
            builder.Services.AddAutoMapper(typeof(MappingProfile));
           
            // Register dependencies
            builder.Services.RegisterDependencies();
            DependencyInjectionConfig.RegisterRepository(builder.Services);
            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
