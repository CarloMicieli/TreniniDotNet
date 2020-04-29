using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Web.UserProfiles.Identity;

namespace TreniniDotNet.Web.UserProfiles.V1.Accounts.AuthenticateUser
{
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AuthenticateController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokensService _tokensService;

        public AuthenticateController(UserManager<ApplicationUser> userManager, ITokensService tokensService)
        {
            _userManager = userManager;
            _tokensService = tokensService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(login.Username);
            if (user != null)
            {
                bool validPassword = await _userManager.CheckPasswordAsync(user, login.Password);
                if (validPassword)
                {
                    return Ok(new
                    {
                        token = _tokensService.CreateToken(user.UserName)
                    });
                }
            }

            return Unauthorized();
        }
    }
}