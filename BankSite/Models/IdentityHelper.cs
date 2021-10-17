using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankSite.Models
{
    public static class IdentityHelper
    {
        public const string AccountHolder = "AccountHolder";
        public const string Manager = "Manager";

        public static async Task CreateRolesAsync(IServiceProvider provider, params string[] roles)
        {
            RoleManager<IdentityRole> roleManager = provider.GetService<RoleManager<IdentityRole>>();

            foreach (string role in roles)
            {
                bool doesRoleExist = await roleManager.RoleExistsAsync(role);
                if (!doesRoleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        public static async Task CreateDefaultUserAsync(IServiceProvider provider, string role)
        {
            var userManager = provider.GetService<UserManager<ApplicationUser>>();

            // if no users are in specified role, make default user in that role
            int numUsers = (await userManager.GetUsersInRoleAsync(role)).Count;
            if (numUsers == 0)
            {
                var defaultUser = new ApplicationUser()
                {
                    Email = "manager@bank.com",
                    UserName = "Admin",
                    EmailConfirmed = true,
                    FirstName = "John",
                    LastName = "Doe"
                };

                await userManager.CreateAsync(defaultUser, "Password123#");

                await userManager.AddToRoleAsync(defaultUser, role);
            }
        }
    }
}
