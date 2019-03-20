using eGoatDDD.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using eGoatDDD.Persistence.Repository;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace eGoatDDD.Persistence.Service.User
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public object ClaimsType { get; private set; }

        public UserService(
           IHttpContextAccessor httpContextAccessor,
        IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        #region User
        /// <summary>
        /// Get the current user asynchronously
        /// </summary>
        /// <param name="principal">HttpContext.User is a good principal claim candidate</param>
        /// <returns></returns>
        public async Task<ApplicationUser> GetUserAsync(ClaimsPrincipal principal)
        {
            return await _userManager.GetUserAsync(principal);
        }

        public async Task<ApplicationUser> FindByNameAsync(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<ApplicationUser> FindByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }
        #endregion
               

        #region Roles
        public async Task AddUserToRolesAsync(ApplicationUser user, List<string> roles)
        {
            await _userManager.AddToRolesAsync(user, roles);
        }

       

        public async Task<List<string>> GetUserRoles()
        {
            return await _roleManager.Roles.Select(x => x.Name).ToListAsync();
        }

        public async Task<IList<string>> GetUserRolesByGuid(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<List<ApplicationUser>> GetListRoleOfUser(string role)
        {
            var userList = await _userManager.GetUsersInRoleAsync(role);
            return userList.ToList();
        }
        #endregion


    }
}
