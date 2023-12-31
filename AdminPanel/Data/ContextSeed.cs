﻿#nullable disable

using Microsoft.AspNetCore.Identity;

namespace AdminPanel.Data
{
    public class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(ApplicationRoles.Roles.Admin.ToString()));

        }

        public static async Task SeedSuperAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                FirstName = "SAMUEL",
                FullName = "SAMUEL " + "ENAKHE " + "IZUAGBE",
                LastName = "IZUAGBE",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Samcooper$01");
                    await userManager.AddToRoleAsync(defaultUser, ApplicationRoles.Roles.Admin.ToString());
                }

            }
        }
    }
}
