using System.Threading.Tasks;
using eGoatDDD.Web.Handler;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eGoatDDD.Web.Controllers
{
        [Route("api/image")]
        public class ImagesController : Controller
        {
            private readonly IImageHandler _imageHandler;

            public ImagesController(IImageHandler imageHandler)
            {
                _imageHandler = imageHandler;
            }

            /// <summary>
            /// Uplaods an image to the server.
            /// </summary>
            /// <param name="file"></param>
            /// <returns></returns>
            public async Task<IActionResult> UploadImage(IFormFile file)
            {
                return await _imageHandler.UploadImage(file);
            }
        }

}
