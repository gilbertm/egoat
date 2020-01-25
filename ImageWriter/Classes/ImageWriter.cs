using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ImageWriter.Helper;
using ImageWriter.Interface;
using Microsoft.AspNetCore.Http;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace ImageWriter.Classes
{
    public class ImageWriter : IImageWriter
    {
        public async Task<string> UploadImage(IFormFile file, string folderLocation)
        {
            if (CheckIfImageFile(file))
            {
                return await WriteFile(file, folderLocation);
            }

            return "Invalid image file";
        }

        /// <summary>
        /// Method to check if file is image file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private bool CheckIfImageFile(IFormFile file)
        {
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();
            }

            return WriterHelper.GetImageFormat(fileBytes) != WriterHelper.ImageFormat.unknown;
        }

        /// <summary>
        /// Method to write file onto the disk
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<string> WriteFile(IFormFile file, string folderLocation)
        {
            string fileName;
            try
            {
                var path = Path.Combine(
                            Directory.GetCurrentDirectory(), folderLocation);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];

                fileName = Guid.NewGuid().ToString() + extension; //Create a new Name 
                                                                  //for the file due to security reasons.
                var pathWithFile = Path.Combine(path,fileName);

                using (var stream = new FileStream(pathWithFile, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                /* https://devblogs.microsoft.com/dotnet/net-core-image-processing
                 * resizing done at the create goat handler for now.
                
                if (File.Exists(pathWithFile))
                {
                    const int size = 256;
                    const int quality = 75;

                    using (var image = new Bitmap(Image.FromFile(pathWithFile)))
                    {
                        int width, height;

                        if (image.Width > image.Height)
                        {
                            width = size;
                            height = Convert.ToInt32(image.Height * size / (double)image.Width);
                        }
                        else
                        {
                            width = Convert.ToInt32(image.Width * size / (double)image.Height);
                            height = size;
                        }
                        var resized = new Bitmap(width, height);
                        using (var graphics = Graphics.FromImage(resized))
                        {
                            graphics.CompositingQuality = CompositingQuality.HighSpeed;
                            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            graphics.CompositingMode = CompositingMode.SourceCopy;
                            graphics.DrawImage(image, 0, 0, width, height);
                            using (var output = File.Open(Path.Combine(path,"resized",fileName), FileMode.Create))
                            {
                                var qualityParamId = System.Drawing.Imaging.Encoder.Quality;
                                var encoderParameters = new EncoderParameters(1);
                                encoderParameters.Param[0] = new EncoderParameter(qualityParamId, quality);
                                var codec = ImageCodecInfo.GetImageDecoders().FirstOrDefault(codec => codec.FormatID == ImageFormat.Jpeg.Guid);
                                resized.Save(output, codec, encoderParameters);
                            }
                        }
                    }
                } */

            }
            catch (Exception e)
            {
                return e.Message;
            }

            return fileName;
        }

        private string OutputPath(string path, object outputDirectory, object systemDrawing)
        {
            throw new NotImplementedException();
        }
    }
}