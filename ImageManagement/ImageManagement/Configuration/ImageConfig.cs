namespace ImageManagement.Configuration
{
    public class ImageConfig
    {
        public const string ImageSizesSection = "Image.Setting:version1:ImageSizes";
        public const string ImageSettingsSection = "Image.Setting:version1";

        public const string Large = "Large";
        public const string Medium = "Medium";
        public const string Small = "Small";
        public const string Ftp = "FTP";

        public string IPAddress { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string AllowedFileExtensions { get; set; }
        public string MaxContentLength { get; set; }
        public string Height { get; set; }
        public string Width { get; set; }
    }
}