using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TreniniDotNet.Web.UserProfiles.Identity;

namespace TreniniDotNet.IntegrationTests.Helpers.Data
{
    public static class AppIdentityDbContextSeed
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await SeedUserAsync(userManager, "George", "Pa$$word88");
            await SeedUserAsync(userManager, "Ciccins", "Pa$$word88");
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

