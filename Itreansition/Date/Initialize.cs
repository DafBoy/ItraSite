using Microsoft.AspNetCore.Identity;
using Itreansition.Models;
using System.Threading.Tasks;
using Itreansition.Services.HomeSevice;
namespace Initialize
{
    public class StartInitialize
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "6dafboy6@gmail.ru";
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }
            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                string password = "2531495Daf*";
                User admin = new User { Email = adminEmail, UserName = "Admin",EmailConfirmed=true };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                FunctionHelpers.AddAchievement(admin.UserName);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
    }
}