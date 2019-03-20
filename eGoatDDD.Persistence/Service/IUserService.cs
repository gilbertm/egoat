using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using eGoatDDD.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace eGoatDDD.Persistence.Service.User
{
    public interface IUserService
    {
        #region User info, user account
        Task<ApplicationUser> GetUserAsync(ClaimsPrincipal principal);
        Task<ApplicationUser> FindByNameAsync(string username);
        Task<ApplicationUser> FindByEmailAsync(string email);
        Task<ApplicationUser> FindByIdAsync(string id);
        #endregion


        #region Roles
        Task AddUserToRolesAsync(ApplicationUser user, List<string> roles);
        Task<List<ApplicationUser>> GetListRoleOfUser(string role);
        Task<List<string>> GetUserRoles();
        Task<IList<string>> GetUserRolesByGuid(string userId);
        #endregion

       
    }
}
