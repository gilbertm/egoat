using Microsoft.AspNetCore.Http;
using System;
using System.Web;


namespace eGoatDDD.JavascriptConsole
{
    public class Javascript
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public Javascript(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void ConsoleLog(string message)
        {
            string function = "console.log('{0}');";
            string log = string.Format(GenerateCodeFromFunction(function), message);

            _httpContextAccessor.HttpContext.Response.WriteAsync(log);
        }

        public void Alert(string message)
        {
            string function = "alert('{0}');";
            string log = string.Format(GenerateCodeFromFunction(function), message);
            _httpContextAccessor.HttpContext.Response.WriteAsync(log);
        }

        static string GenerateCodeFromFunction(string function)
        {
            return string.Format("<script type=\"text/javascript\" language=\"javascript\">{0}</script>", function);
        }
    }
}

