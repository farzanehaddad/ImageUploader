using ImageManagement.Services.Dto;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ImageManagement.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("/error-production")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult ErrorProduction()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            return BadRequest(new UploadImageResultModel
            {
                ResultModel = new ResultModel()
                {
                    HasError = true,
                    Message = !string.IsNullOrEmpty(context.Error.Message) ?
                                    context.Error.Message : "خطا در سرویس"
                }
            });
        }

        [Route("/error-development")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult ErrorDevelopment([FromServices] IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment.EnvironmentName != "Development")
            {
                throw new InvalidOperationException(
                    "This shouldn't be invoked in non-development environments.");
            }

            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            return Problem(
                detail: context.Error.StackTrace,
                title: context.Error.Message);
        }
    }
}
