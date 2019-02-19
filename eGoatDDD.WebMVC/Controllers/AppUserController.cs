using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using eGoatDDD.Application.AppUsers.Commands;
using eGoatDDD.WebMVC.Infrastructure;
using eGoatDDD.Application.AppUsers.Models;

namespace eGoatDDD.WebMVC.Controllers
{
    public class AppUserController : BaseController
    {
        [HttpGet]
        public IActionResult Update()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromForm] UpdateAppUserCommand command)
        {
            if (ModelState.IsValid)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var user = await _userManager.GetUserAsync(User);

                    AppUserDto AppUser = await _mediator.Send(command);
                }

            }
            
            return Ok();
        }
    }
}
