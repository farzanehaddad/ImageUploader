using System.Collections.Generic;

namespace ImageManagement.Services.Dto
{
    public class UploadImageResultModel
    {
        public ResultModel ResultModel { get; set; }
        public List<string> ImagesName { get; set; }
    }
}