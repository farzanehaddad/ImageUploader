using ImageManagement.Model;
using ImageManagement.Services.Dto;
using Microsoft.Extensions.Logging;
using System.Diagnostics;


namespace ImageManagement.Services
{
    public class LogService : IImageService
    {
        private readonly ILogger<LogService> _logger;
        private readonly IImageService _imageService;

        public LogService(ILogger<LogService> logger,
                          IImageService imageService)
        {
            _logger = logger;
            _imageService = imageService;
        }

        public UploadImageResultModel FTPImage(UploadImageModel input)
        {
            Stopwatch sw = Stopwatch.StartNew();
            var result = _imageService.FTPImage(input);
            sw.Stop();
            _logger.LogWarning($"====> FTP image - elapsed milli seconds : {sw.ElapsedMilliseconds}");
            return result;
        }

        public ResultModel RemoveImage(RemoveImageRichModel input)
        {
            Stopwatch sw = Stopwatch.StartNew();
            var result = _imageService.RemoveImage(input);
            sw.Stop();
            _logger.LogWarning($"====> Removing image - elapsed milli seconds : {sw.ElapsedMilliseconds}");
            return result;
        }

        public UploadImageResultModel UploadImage(UploadImageModel input)
        {
            Stopwatch sw = Stopwatch.StartNew();
            var result = _imageService.UploadImage(input);
            sw.Stop();
            _logger.LogWarning($"====> Uploading image - elapsed milli seconds : {sw.ElapsedMilliseconds}");
            return result;
        }
    }
}
