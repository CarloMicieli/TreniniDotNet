using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TreniniDotNet.Web.Root.V1
{
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}")]
    [ApiController]
    public sealed class RootController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetRoot()
        {
            return Ok(new
            {
                brand = "https://localhost:5001/api/v1/brands",
                catalogItems = "https://localhost:5001/api/v1/catalogItems",
                railways = "https://localhost:5001/api/v1/railways",
                scales = "https://localhost:5001/api/v1/scales",
            });
        }
    }
}
