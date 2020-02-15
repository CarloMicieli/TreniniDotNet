using Microsoft.AspNetCore.Mvc;

namespace TreniniDotNet.WebUi.UseCases.V1.Health
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult HealthCheck()
        {
            return Ok();
        }
    }
}