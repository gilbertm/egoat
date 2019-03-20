using eGoatDDD.Application.Users.Manages.Commands;
using eGoatDDD.Application.Users.Manages.Models;
using eGoatDDD.Application.Users.Manages.Queries;
using eGoatDDD.Domain.Entities;
using eGoatDDD.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eGoatDDD.Web.Controllers.Manage
{
    // [Authorize(Policy = "Administrators")]
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

            ApplicationUser user = await _userManager.FindByIdAsync("c7f5cb53-4494-41ce-9257-eb00932ff8f8");

            // await _userManager.Remove.RemoveFromRolesAsync(user, new string[] { "Supervisor" });

            // await _userManager.AddToRolesAsync(user, new string[] { "Administrator" });

            await _mediator.Send(new UpdateRolesCommand(userRolesViewModel));

            return RedirectToAction("Index");
        }

        /*public async Task ManageRoles(SelectOptionList roles)
        {
            var roleList = await GetUserRoles();


            //when add new role value and label will be the same
            var roleToAdd = roles.SelectOptionViewModels.Where(x => x.Label == x.Value).ToList();
            var roleToRemove = roleList.Where(existingRole => roles.SelectOptionViewModels
                .All(item => item.Value != existingRole.Id && existingRole.Name != "Administrator")).ToList();

            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    if (roleToRemove.Any())
                    {
                        var roleName = roleToRemove.Select(x => x.Name).ToArray();
                        foreach (var roleRemove in roleToRemove)
                        {
                            var userInRole = await _userService.GetListRoleOfUser(roleRemove.Name);
                            if (!userInRole.Any()) continue;
                            foreach (var user in userInRole)
                            {
                                await _userService.RemoveFromRolesAsync(user, roleName);
                            }

                            var role = await _unitOfWork.Repository<IdentityRole>().FindAsync(x =>
                                x.Name.Equals(roleRemove.Name, StringComparison.OrdinalIgnoreCase));
                            await _unitOfWork.Repository<IdentityRole>().DeleteAsync(role);
                        }
                    }

                    if (roleToAdd.Any())
                    {
                        var roleName = roleToAdd.Select(x => x.Label).ToArray();
                        await _userService.AddUserRoles(roleName);
                    }

                    transaction.Complete();
                }
                catch (Exception)
                {
                    _unitOfWork.Rollback();
                }
            }
        } */
    }
}