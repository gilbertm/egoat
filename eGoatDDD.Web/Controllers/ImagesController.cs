using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using eGoatDDD.Web.Handler;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eGoatDDD.Web.Controllers
{
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
        [Route("api/image")]
        public async Task<string> UploadImage(IFormFile file)
        {
            return await _imageHandler.UploadImage(file, "wwwroot\\images\\uploads");
        }

       

    }

}
