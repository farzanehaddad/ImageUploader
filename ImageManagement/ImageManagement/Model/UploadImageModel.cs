using System;
using System.IO;
using ImageManagement.Configuration;
using ImageManagement.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace ImageManagement.Model
{
    public class UploadImageModel : BaseModel
    {
        private readonly ImageConfig _imageConfig;

        public UploadImageModel(IOptionsSnapshot<ImageConfig> imageConfig)
        {
            _imageConfig = imageConfig.Value;
        }

        public IFormFile File { get; set; }
        public string FilePath { get; set; }
        public int CompressionRate { get; set; }


        #region ValidatedFile

        private IFormFile _validatedFile { get; set; }
        public IFormFile ValidatedFile
        {
            get
            {
                if (_validatedFile == null)
                    SetValidatedFile();
                return _validatedFile;
            }
        }
        private void SetValidatedFile()
        {
            if (File != null &&
                !_imageConfig.AllowedFileExtensions.Contains(File.FileName.Substring(File.FileName.LastIndexOf('.')).ToLower()))
            {
                throw new Exception($"فرمت فایل صحیح نیست.");
            }

            if (File != null && File.Length > Convert.ToInt32(_imageConfig.MaxContentLength))
            {
                throw new Exception($"حجم فایل بیشتر از {_imageConfig.MaxContentLength} mb است.");
            }
            _validatedFile = File;
        }

        #endregion

        #region QualityRate

        private int _qualityRate { get; set; }
        public int QualityRate
        {
            get
            {
                SetQualityRate();
                return _qualityRate;
            }
        }
        private void SetQualityRate()
        {
            _qualityRate = MaxCompressionRate - CompressionRate;
        }

        #endregion

        #region FileName

        private string _fileName { get; set; }
        public string FileName
        {
            get
            {
                if (string.IsNullOrEmpty(_fileName))
                    SetFileName();

                return _fileName;
            }
        }
        private void SetFileName()
        {
            _fileName = DateTime.Now.ToPersianDate() + "-" + File.FileName;
        }

        #endregion

        #region FilePathOriginal

        private string _filePathOriginal { get; set; }
        public string FilePathOriginal
        {
            get
            {
                if (string.IsNullOrEmpty(_filePathOriginal))
                    SetFilePathOriginal();

                return _filePathOriginal;
            }
        }
        private void SetFilePathOriginal()
        {
            var p = FilePath == null ? "Images" : Path.Combine("Images", FilePath);
            _filePathOriginal = Path.Combine(p, "original-" + FileName);
        }

        #endregion

        #region FilePathSmall

        private string _filePathSmall { get; set; }
        public string FilePathSmall
        {
            get
            {
                if (string.IsNullOrEmpty(_filePathSmall))
                    SetFilePathSmall();

                return _filePathSmall;
            }
        }
        private void SetFilePathSmall()
        {
            var p = FilePath == null ? "Images" : Path.Combine("Images", FilePath);
            _filePathSmall = Path.Combine(p, "small-" + FileName);
        }

        #endregion

        #region FilePathCompressed

        private string _filePathCompressed { get; set; }
        public string FilePathCompressed
        {
            get
            {
                if (string.IsNullOrEmpty(_filePathCompressed))
                    SetFilePathCompressed();

                return _filePathCompressed;
            }
        }
        private void SetFilePathCompressed()
        {
            var p = FilePath == null ? "Images" : Path.Combine("Images", FilePath);
            _filePathCompressed = Path.Combine(p, "compressed-" + FileName);
        }

        #endregion

    }
}

