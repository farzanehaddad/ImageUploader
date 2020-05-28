using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace ImageManagement.Model
{
    public class UploadImageDataContract : BaseModel, IValidatableObject
    {
        public IFormFile File { get; set; }
        public string FilePath { get; set; }
        public int CompressionRate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var fileErrors = ValidateFile();
            if (!string.IsNullOrEmpty(fileErrors))
                yield return new ValidationResult(fileErrors, new List<string> { nameof(File) });

            var filePathErrors = ValidateFilePath();
            if (!string.IsNullOrEmpty(filePathErrors))
                yield return new ValidationResult(filePathErrors, new List<string> { nameof(FilePath) });

            var compressionRateErrors = ValidateCompressionRate();
            if (!string.IsNullOrEmpty(compressionRateErrors))
                yield return new ValidationResult(compressionRateErrors, new List<string> { nameof(CompressionRate) });

        }

        #region Methods

        private string ValidateFile()
        {
            var error = "";

            if (File == null)
            {
                error += ($"فایل را انتخاب کنید.");
            }

            if (File != null && File.Length <= default(int))
            {
                error += ($"فایل خالی است.");
            }

            return error;
        }

        private string ValidateFilePath()
        {
            var error = "";
            try
            {
                Directory.CreateDirectory(FilePath != null ?
                    Path.Combine("Images", FilePath) : "Images");
            }
            catch (Exception)
            {
                error += ($"نام دایرکتوری صحیح نیست.");
            }

            return error;
        }

        private string ValidateCompressionRate()
        {
            var error = "";

            if (Convert.ToInt32(CompressionRate) < default(int) || Convert.ToInt32(CompressionRate) > MaxCompressionRate)
            {
                error += ($"برای تعیین میزان فشردگی تصویر، عددی مابین صفر تا صد وارد کنید");
            }
            return error;
        }

        #endregion

    }
}
