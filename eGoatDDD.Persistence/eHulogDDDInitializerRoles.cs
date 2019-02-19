using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace eGoatDDD.Persistence
{
    public class eGoatDDDInitializerRoles
    {
        private static readonly string[] Roles = new string[] { "Administrator", "Lender", "Lessee" };

        public static async Task SeedRoles(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<eGoatDDDDbContext>();

                if (!dbContext.UserRoles.Any())
                {
                    var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                    
                    foreach (var role in Roles)
                    {
                        if (!await roleManager.RoleExistsAsync(role))
                        {
                            await roleManager.CreateAsync(new IdentityRole(role));
                        }
                    }
                }

            }
        }

    }
}
