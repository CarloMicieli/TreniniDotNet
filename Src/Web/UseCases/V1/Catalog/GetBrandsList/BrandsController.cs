using Microsoft.AspNetCore.Mvc;

namespace Web.UseCases.V1.Catalog.GetBrandsList
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]    
    public class BrandsController : ControllerBase
    {
    }
}