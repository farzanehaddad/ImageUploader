using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ImageManagement.Model
{
    public class FtpImageDataContract : BaseModel, IValidatableObject
    {
        public IFormFile File { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var fileErrors = ValidateFile();
            if (!string.IsNullOrEmpty(fileErrors))
                yield return new ValidationResult(fileErrors, new List<string> { nameof(File) });
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

        #endregion

    }
}
