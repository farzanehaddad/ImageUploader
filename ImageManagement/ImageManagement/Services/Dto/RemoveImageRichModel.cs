using System.IO;

namespace ImageManagement.Services.Dto
{
    public class RemoveImageRichModel
    {
        private string _fileName;

        public string FileName
        {
            get => _fileName;
            set
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "Images");
                _fileName = Path.Combine(path, value);
            }
        }
    }
}