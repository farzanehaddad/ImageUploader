using ImageManagement.Model;

namespace ImageManagement.Extensions
{
    public static class UploadImageModelExtensions
    {
        public static UploadImageModel PrepareModel(this UploadImageModel uploadImageModel, UploadImageDataContract input)
        {
            uploadImageModel.File = input.File;
            uploadImageModel.CompressionRate = input.CompressionRate;
            uploadImageModel.FilePath = input.FilePath;
            return uploadImageModel;
        }

        public static UploadImageModel PrepareModel(this UploadImageModel uploadImageModel, FtpImageDataContract input)
        {
            uploadImageModel.File = input.File;
            return uploadImageModel;
        }
    }
}
