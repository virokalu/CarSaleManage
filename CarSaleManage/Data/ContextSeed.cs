using CarSaleManage.Models;
using Microsoft.AspNetCore.Identity;

namespace CarSaleManage.Data
{
    public static class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new AppUser
            {
                UserName = "virokemin@gmail.com",
                Email = "virokemin@gmail.com",
                Firstname = "Viro",
                Lastname = "Kemin",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {

                    var results = await userManager.CreateAsync(defaultUser, "123Pa$$word.");

                    //Seed Roles
                    await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Admin.ToString()));
                    await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Manager.ToString()));
                    await roleManager.CreateAsync(new IdentityRole(Enums.Roles.User.ToString()));

                    if (results.Succeeded)
                    {
                        await userManager.AddToRoleAsync(defaultUser, Data.Enums.Roles.Admin.ToString());
                    }

                }
            }
        }
    }
}
