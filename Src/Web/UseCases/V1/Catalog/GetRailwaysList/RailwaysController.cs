using Microsoft.AspNetCore.Mvc;

namespace Web.UseCases.V1.Catalog.GetRailwaysList
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class RailwaysController : ControllerBase
    {
    }
}