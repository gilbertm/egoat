using eGoatDDD.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using eGoatDDD.Application.Users.Manages.Queries;
using eGoatDDD.Application.Users.Manages.Models;
using eGoatDDD.Application.Users.Manages.Commands;
using Microsoft.AspNetCore.Authorization;

namespace eGoatDDD.Web.Controllers.Manage
{
    [Authorize(Policy = "Administrators")]
    public class UserController : BaseController
    {
        [Route("manage/users")]
        public async Task<IActionResult> Index()
        {
            var usersWithRoles = await _mediator.Send(new GetUsersQuery());

            return View(usersWithRoles);
        }

        [HttpPost]
        [Route("manage/users/update")]
        public async Task<IActionResult> Update(UserRolesViewModel userRolesViewModel, string Update = null)
        {
            switch (Update)
            {
                case "Update":
                    await _mediator.Send(new UpdateRolesCommand(userRolesViewModel));
                    break;
                case "Delete":
                    await _mediator.Send(new DeleteUserCommand(userRolesViewModel));
                    break;
                default:
                    await _mediator.Send(new UpdateRolesCommand(userRolesViewModel));
                    break;
            }

            return RedirectToAction("Index");
        }
    }
}