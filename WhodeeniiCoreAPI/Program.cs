using FluentAssertions.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WhoDeenii.API.Extensions;
using WhoDeenii.Domain.Contracts.Interfaces;
using WhoDeenii.Domain.Services.Services;
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
            //builder.Logging.ClearProviders(); 
            //builder.Logging.AddConsole();

            // Add services to the container.
            builder.Services.AddDbContext<WhoDeeniiDbContext>(options =>
                   options.UseSqlServer(builder.Configuration.GetConnectionString("WhoDeenii")));

            // Configure ImageSettings
            builder.Services.Configure<ImageSettings>(builder.Configuration.GetSection("ImageSettings"));

            var jwtSettings = builder.Configuration.GetSection("JwtSettings");
            var key = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"]);

            // Configure authentication with JWT
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true, // Validate the signing key used to create the token
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"])), // Use the secret key from configuration
                     ValidateIssuer = true, // Validate the issuer of the token
                     ValidIssuer = builder.Configuration["JwtSettings:Issuer"], // Set the expected issuer
                     ValidateAudience = true, // Validate the intended recipient of the token
                     ValidAudience = builder.Configuration["JwtSettings:Audience"], // Set the expected audience
                     ValidateLifetime = true, // Validate the token expiration time
                     ClockSkew = TimeSpan.Zero // Set clock skew to zero for strict time validation (optional)
                 };
             });

            // Register the Swagger generator, defining one or more Swagger documents
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "JWTToken_Auth_API",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            builder.Services.AddControllers();
            // Add AutoMapper
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            // Register dependencies
            builder.Services.RegisterDependencies(builder.Configuration);
            DependencyInjectionConfig.RegisterRepository(builder.Services);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
