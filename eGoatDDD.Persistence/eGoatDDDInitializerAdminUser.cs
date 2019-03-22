using eGoatDDD.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eGoatDDD.Persistence
{
    public class eGoatDDDInitializerAdminUser
    {
        public static async Task SeedAdminUserAsync(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByEmailAsync("adm1n1str8tor@hnm-goat-empire.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "adm1n1str8tor@hnm-goat-empire.com",
                    Email = "adm1n1str8tor@hnm-goat-empire.com",
                    FirstName = "Administrator",
                    MiddleName = "Administrator",
                    LastName = "Administrator"
                };

                IdentityResult result = userManager.CreateAsync(user, "$$ZZ@@Zth04112012@@ZZ$$").Result;

                if (result.Succeeded)
                {
                    string role = "Administrator";
                    userManager.AddToRoleAsync(user, role).Wait();

                    await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, role));

                }
            }
        }

    }
}
