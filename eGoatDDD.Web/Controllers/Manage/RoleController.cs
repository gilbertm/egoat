using eGoatDDD.Application.Users.Manages.Commands;
using eGoatDDD.Application.Users.Manages.Models;
using eGoatDDD.Application.Users.Manages.Queries;
using eGoatDDD.Web.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eGoatDDD.Web.Controllers.Manage
{
    [Authorize(Policy = "Administrators")]
    [Route("manage/user/roles")]
    public class RoleController : BaseController
    {
        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {

                var usersRoles = await _mediator.Send(new GetUsersRolesQuery());

                System.Diagnostics.Debug.WriteLine(usersRoles);

                return View(usersRoles);
               
            }

            return View();
        }

        [HttpPost]
        [Route("manage/user/roles/update")]
        public async Task<IActionResult> Update(UserRolesViewModel userRolesViewModel) {

            await _mediator.Send(new UpdateRolesCommand(userRolesViewModel));

            return RedirectToAction("Index");
        }

    }
}