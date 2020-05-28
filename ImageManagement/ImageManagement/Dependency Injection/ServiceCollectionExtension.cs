using ImageManagement.Configuration;
using ImageManagement.Model;
using ImageManagement.Services;
using ImageManagement.Services.Dto;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace ImageManagement.Dependency_injection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddImageManagementServices(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();

            services.AddTransient<RemoveImageRichModel>();
            services.AddTransient<UploadImageModel>();
            //services.AddTransient<ImageConfig>();
            services.Configure<ImageConfig>(ImageConfig.Ftp, configuration.GetSection(ImageConfig.ImageSettingsSection));

            services.AddTransient<IImageService>(provider =>
            {
                var logger = provider.GetService<ILogger<LogService>>();
                var config = provider.GetService<ImageConfig>();

                IImageService imageService = new ImageService(config);
                return new LogService(logger, imageService);
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v2",
                    Title = "Image Management API",
                    Contact = new OpenApiContact
                    {
                        Name = "Farzane haddad",
                        Email = "fbadr88@gmail.com"
                    }
                });
            });

            return services;
        }
    }
}