using ImageManagement.Configuration;
using ImageManagement.Model;
using ImageManagement.Services;
using ImageManagement.Services.Dto;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
            services.Configure<ImageSize>(ImageSize.Small,
                configuration.GetSection($"{ImageSize.ImageSizesSection}:{ImageSize.Small}"));
            services.Configure<ImageConfig>(configuration.GetSection(ImageConfig.ImageSettingsSection));
            services.Configure<FtpConfig>(configuration.GetSection(FtpConfig.ImageFtpSection));

            services.AddTransient<IImageService>(provider =>
            {
                var logger = provider.GetService<ILogger<LogService>>();
                var ftpConfig = provider.GetService<IOptionsSnapshot<FtpConfig>>();
                var imageSize = provider.GetService<IOptionsSnapshot<ImageSize>>();

                IImageService imageService = new ImageService(imageSize, ftpConfig);
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