using eGoatDDD.Domain.Entities;
using eGoatDDD.WebMVC.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace eGoatDDD.WebMVC.Controllers
{
    [Authorize(Policy = "Lessees")]
    public class LesseeController : BaseController
    {

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            
            return View();
        }

    }
}