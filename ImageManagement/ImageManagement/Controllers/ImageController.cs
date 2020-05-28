using ImageManagement.Model;
using Microsoft.AspNetCore.Mvc;
using ImageManagement.Services;
using ImageManagement.Services.Dto;
using ImageManagement.Extensions;

namespace ImageManagement.Controllers
{
    [ApiController]
    [Route("api/v/[controller]")]
    //[Authorize(Policy = "Management")]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;
        private readonly RemoveImageRichModel _removeImageRichModel;
        private readonly UploadImageModel _uploadImageModel;


        public ImageController(IImageService imageService,
                                RemoveImageRichModel removeImageRichModel,
                                UploadImageModel uploadImageModel)
        {
            _imageService = imageService;
            _removeImageRichModel = removeImageRichModel;
            _uploadImageModel = uploadImageModel;
        }

        [HttpPost]
        [Route("")]
        public IActionResult UploadImage([FromForm]UploadImageDataContract model)
        {
            var result = _imageService.UploadImage(_uploadImageModel.PrepareModel(model));

            if (result.ResultModel.HasError)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost]
        [Route("/ftp")]
        public IActionResult FtpImage([FromForm]FtpImageDataContract model)
        {
            var result = _imageService.FTPImage(_uploadImageModel.PrepareModel(model));

            if (result.ResultModel.HasError)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpDelete]
        [Route("")]
        public IActionResult RemoveImage([FromQuery]string fileName)
        {
            _removeImageRichModel.FileName = fileName;

            var result = _imageService.RemoveImage(_removeImageRichModel);
            if (result.HasError)
                return BadRequest(result.Message);
            return Ok(result);
        }
    }
}
