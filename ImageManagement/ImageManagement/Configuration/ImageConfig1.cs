using Microsoft.Extensions.Configuration;

namespace ImageManagement.Configuration
{
    public sealed class ImageConfig1
    {
        private readonly IConfiguration _configuration;

        private const string AllowedFileExtensionsKey = "Image.Setting:version1:AllowedFileExtensions";
        private const string MaxContentLengthKey = "Image.Setting:version1:MaxContentLength";
        private const string SmallSizeHeightKey = "Image.Setting:version1:ImageSizes:Small:Height";
        private const string SmallSizeWidthKey = "Image.Setting:version1:ImageSizes:Small:Width";
        private const string FtpUsernameKey = "Image.Setting:version1:FTP:Username";
        private const string FtpPasswordKey = "Image.Setting:version1:FTP:Password";
        private const string FtpIPAddressKey = "Image.Setting:version1:FTP:IPAddress";


        public ImageConfig1(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region FtpIPAddress

        private string _ftpIPAddress;
        public string FtpIPAddress
        {
            get
            {
                if (string.IsNullOrEmpty(_ftpIPAddress))
                {
                    SetFtpIPAddress();
                }

                return _ftpIPAddress;
            }
        }
        private void SetFtpIPAddress()
        {
            _ftpIPAddress = _configuration[FtpIPAddressKey];
        }

        #endregion

        #region FtpPassword

        private string _ftpPassword;
        public string FtpPassword
        {
            get
            {
                if (string.IsNullOrEmpty(_ftpPassword))
                {
                    SetFtpPassword();
                }

                return _ftpPassword;
            }
        }
        private void SetFtpPassword()
        {
            _ftpPassword = _configuration[FtpPasswordKey];
        }

        #endregion


        #region FtpUsername

        private string _ftpUsername;
        public string FtpUsername
        {
            get
            {
                if (string.IsNullOrEmpty(_ftpUsername))
                {
                    SetFtpUsername();
                }

                return _ftpUsername;
            }
        }
        private void SetFtpUsername()
        {
            _ftpUsername = _configuration[FtpUsernameKey];
        }

        #endregion


        #region AllowedFileExtensions

        private string _allowedFileExtensions;
        public string AllowedFileExtensions
        {
            get
            {
                if (string.IsNullOrEmpty(_allowedFileExtensions))
                {
                    SetAllowedFileExtensions();
                }

                return _allowedFileExtensions;
            }
        }
        private void SetAllowedFileExtensions()
        {
            _allowedFileExtensions = _configuration[AllowedFileExtensionsKey];
        }

        #endregion

        #region MaxContentLength

        private string _maxContentLength;
        public string MaxContentLength
        {
            get
            {
                if (string.IsNullOrEmpty(_maxContentLength))
                {
                    SetMaxContentLength();
                }

                return _maxContentLength;
            }
        }
        private void SetMaxContentLength()
        {
            _maxContentLength = _configuration[MaxContentLengthKey];
        }

        #endregion

        #region SmallSizeHeight

        private string _smallSizeHeight;
        public string SmallSizeHeight
        {
            get
            {
                if (string.IsNullOrEmpty(_smallSizeHeight))
                {
                    SetSmallSizeHeight();
                }

                return _smallSizeHeight;
            }
        }
        private void SetSmallSizeHeight()
        {
            _smallSizeHeight = _configuration[SmallSizeHeightKey];
        }

        #endregion

        #region SmallSizeWidth

        private string _smallSizeWidth;
        public string SmallSizeWidth
        {
            get
            {
                if (string.IsNullOrEmpty(_smallSizeWidth))
                {
                    SetSmallSizeWidth();
                }

                return _smallSizeWidth;
            }
        }
        private void SetSmallSizeWidth()
        {
            _smallSizeWidth = _configuration[SmallSizeWidthKey];
        }

        #endregion
    }
}