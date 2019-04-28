using eGoatDDD.Application.GoatResources.Queries;
using eGoatDDD.JavascriptConsole;
using eGoatDDD.Web.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace eGoatDDD.Web.Controllers.Manage
{
    [Authorize(Policy = "Administrators")]
    public class FileController : BaseController
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FileController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [Route("manage/files/clean")]
        public IActionResult Clean()
        {
            Javascript js = new Javascript(_httpContextAccessor);

            try
            {
                // Set a variable to the My Documents path.
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

                if (Directory.Exists(path))
                {

                    var files = Directory.EnumerateFiles(path, "*.*", SearchOption.AllDirectories);

                    foreach (var f in files)
                    {
                        if (System.IO.File.Exists(f))
                        {
                            var fileName = Path.GetFileNameWithoutExtension(f);

                            bool isResource = _mediator.Send(new GetGoatResourceQuery(fileName)).Result;

                            // js.ConsoleLog($"{fileName} file found.");
                            System.Diagnostics.Debug.WriteLine($"{fileName} file found.");

                            if (!isResource)
                            {
                                System.IO.File.Delete(f);
                                // js.ConsoleLog($"{fileName} deleted.");
                                System.Diagnostics.Debug.WriteLine($"{fileName} deleted.");

                            }
                        }
                    }

                }

            }
            catch (UnauthorizedAccessException uAEx)
            {
                System.Diagnostics.Debug.WriteLine(uAEx.Message);
            }
            catch (PathTooLongException pathEx)
            {
                System.Diagnostics.Debug.WriteLine(pathEx.Message);
            }

            return RedirectToAction("Index", "Goat");

        }
    }
}