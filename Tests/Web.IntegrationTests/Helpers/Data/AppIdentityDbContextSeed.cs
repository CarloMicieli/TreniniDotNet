using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TreniniDotNet.Web.Identity;

namespace TreniniDotNet.IntegrationTests.Helpers.Data
{
    public static class AppIdentityDbContextSeed
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await SeedUserAsync(userManager, "george@gmail.com", "Rocket$$89");
        }

        private static Task SeedUserAsync(UserManager<ApplicationUser> userManager, string username, string password)
        {
            var newUser = new ApplicationUser
            {
                Email = username,
                UserName = username,
            };

            return userManager.CreateAsync(newUser, password);
        }
    }
}

