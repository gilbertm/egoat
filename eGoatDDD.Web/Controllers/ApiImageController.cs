using eGoatDDD.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using eGoatDDD.Web.Handler;

namespace eGoatDDD.Web.Controllers
{
    public class ApiImageController : BaseController
    {

        private readonly IImageHandler _imageHandler;

        public ApiImageController(IImageHandler imageHandler)
        {
            _imageHandler = imageHandler;
        }

        /// <summary>
        /// Uplaods an image to the server.
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        [Route("api/images")]
        public async Task<IActionResult> MultipleUploadImage(IList<IFormFile> files)
        {
            List<string> fileNames = new List<string>();



            foreach (IFormFile file in files)
            {
                if (file == null || file.Length == 0)
                {
                    continue;
                }

                var result = await _imageHandler.UploadImage(file, "tmp");

                fileNames.Add(result.ToString());
            }

            if (fileNames != null)
                return Json(new { error = 0, response = fileNames });

            return Json(new { error = 1, response = "Goat does not exists." });
        }

    }

}
