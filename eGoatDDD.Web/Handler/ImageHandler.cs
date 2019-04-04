using System.Threading.Tasks;
using ImageWriter.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eGoatDDD.Web.Handler
{
    public interface IImageHandler
    {
        Task<string> UploadImage(IFormFile file, string folderLocation);
    }

    public class ImageHandler : IImageHandler
    {
        private readonly IImageWriter _imageWriter;
        public ImageHandler(IImageWriter imageWriter)
        {
            _imageWriter = imageWriter;
        }

        public async Task<string> UploadImage(IFormFile file, string folderLocation)
        {
            var result = await _imageWriter.UploadImage(file, folderLocation);
            return result;
        }
    }
}