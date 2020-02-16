using Microsoft.AspNetCore.Mvc;

namespace Web.UseCases.V1.Catalog.GetScalesList
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ScalesController : ControllerBase
    {
    }
}