using System;
using System.Collections.Generic;
using System.IO;
using ImageManagement.Configuration;
using ImageManagement.Services.Dto;
using System.Drawing;
using ImageMagick;
using ImageManagement.Model;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace ImageManagement.Services
{
    public interface IImageService
    {
        UploadImageResultModel UploadImage(UploadImageModel input);
        ResultModel RemoveImage(RemoveImageRichModel input);
        UploadImageResultModel FTPImage(UploadImageModel input);
    }

    public class ImageService : BaseModel, IImageService
    {
        private readonly ImageConfig _config;

        public ImageService(ImageConfig config)
        {
            _config = config;
        }

        public UploadImageResultModel FTPImage(UploadImageModel input)
        {
            UploadImageToLocalServer(input.ValidatedFile, input.FilePathOriginal);

            var buff = new byte[buffLength];
            int contentLen;
            var fileInf = new FileInfo(input.FilePathOriginal);
            var uri = $"ftp://{_config.FtpIPAddress}/{fileInf.Name}";

            FtpWebRequest reqFTP;
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
            reqFTP.Credentials = new NetworkCredential(_config.FtpUsername, _config.FtpPassword);
            reqFTP.KeepAlive = false;
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
            reqFTP.UseBinary = true;
            reqFTP.ContentLength = fileInf.Length;

            var fs = fileInf.OpenRead();

            var strm = reqFTP.GetRequestStream();
            contentLen = fs.Read(buff, 0, buffLength);
            while (contentLen != 0)
            {
                strm.Write(buff, 0, contentLen);
                contentLen = fs.Read(buff, 0, buffLength);
            }
            strm.Close();
            fs.Close();

            return new UploadImageResultModel
            {
                ResultModel = new ResultModel()
                {
                    HasError = false,
                    Message = "آپلود تصویر با موفقیت انجام شد."
                },
                ImagesName = new List<string> { uri }
            };
        }

        public UploadImageResultModel UploadImage(UploadImageModel input)
        {
            List<string> imagesName = new List<string>();

            UploadImageToLocalServer(input.ValidatedFile, input.FilePathOriginal);
            imagesName.Add(input.FilePathOriginal);

            var compressedImage = CompressImage(input.FilePathOriginal, input.QualityRate);
            SaveImage(compressedImage, input.FilePathCompressed);
            imagesName.Add(input.FilePathCompressed);

            var smallImage = ResizeImage(input.FilePathOriginal,
                Int32.Parse(_config.SmallSizeWidth), Int32.Parse(_config.SmallSizeHeight));
            SaveImage(smallImage, input.FilePathSmall);
            imagesName.Add(input.FilePathSmall);

            return new UploadImageResultModel
            {
                ResultModel = new ResultModel()
                {
                    HasError = false,
                    Message = "آپلود تصویر با موفقیت انجام شد."
                },
                ImagesName = imagesName
            };
        }

        public ResultModel RemoveImage(RemoveImageRichModel input)
        {
            if (File.Exists(input.FileName))
                File.Delete(input.FileName);
            else
            {
                return new ResultModel
                {
                    HasError = true,
                    Message = "تصویر یافت نشد."
                };
            }

            return new ResultModel
            {
                HasError = false,
                Message = "تصویر با موفقیت حذف شد."
            };
        }

        #region Methods

        private void UploadImageToLocalServer(IFormFile file, string filePath)
        {
            using (var image = Image.FromStream(file.OpenReadStream()))
            {
                image.Save(filePath);
            }
        }

        private MagickImage CompressImage(string filePathOriginal, int qualityRate)
        {
            var imageMagic = new MagickImage(filePathOriginal);
            imageMagic.Quality = qualityRate;
            return imageMagic;
        }

        private MagickImage ResizeImage(string filePathOriginal, int width, int height)
        {
            var imageMagic = new MagickImage(filePathOriginal);
            imageMagic.Resize(width, height);
            return imageMagic;
        }

        private void SaveImage(MagickImage image, string filePath)
        {
            image.Write(filePath);
        }

        #endregion
    }
}