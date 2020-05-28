using Microsoft.AspNetCore.Http;

namespace ImageManagement.Services.Dto
{
    public class UploadImageDto 
    {
        public IFormFile File { get; set; }
        public string FileName { get; set; }
        public int QualityRate { get; set; }
        public string FilePathOriginal { get; set; }
        public string FilePathSmall { get; set; }
        public string FilePathCompressed { get; set; }
    }
}