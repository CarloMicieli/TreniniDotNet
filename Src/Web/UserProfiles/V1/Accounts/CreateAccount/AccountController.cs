using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Web.UserProfiles.Identity;

namespace TreniniDotNet.Web.UserProfiles.V1.Accounts.CreateAccount
{
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] Account account)
        {
            if (ModelState.IsValid)
            {
                var newUser = new ApplicationUser
                {
                    Email = account.Email,
                    UserName = account.Username
                };

                var result = await _userManager.CreateAsync(newUser, account.Password);
                if (result.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}