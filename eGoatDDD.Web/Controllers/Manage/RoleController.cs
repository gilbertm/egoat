using eGoatDDD.Application.Users.Manages.Models;
using eGoatDDD.Application.Users.Manages.Queries;
using eGoatDDD.Application.Users.Roles.Models;
using eGoatDDD.Application.Users.Roles.Queries;
using eGoatDDD.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading;
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
                // var user = await _userService.FindByIdAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

                // System.Diagnostics.Debug.WriteLine(user);

                // var getUser = await _userService.GetUserAsync(HttpContext.User);

                // System.Diagnostics.Debug.WriteLine(getUser);


                var usersRoles = await _mediator.Send(new GetUsersRolesQuery());

                System.Diagnostics.Debug.WriteLine(usersRoles);

                return View(usersRoles);
                /* var userRoles = await _mediator.Send(new GetRolesCurrentQuery());


                UserRolesListViewModel userRolesListViewModel = await _mediator.Send(new GetRolesQuery());

                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                RolesUserViewModel rolesUser = await _mediator.Send(new GetRolesByIdQuery(userId)); */
            }

            return View();
        }


        [Route("update/{userRoles}")]
        [HttpPost]
        public async Task<IActionResult> UserRoles(UserRolesViewModel userRoles) {

            UserRolesListViewModel userRolesListViewModel = await _mediator.Send(new GetRolesQuery());
            

            return View();
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