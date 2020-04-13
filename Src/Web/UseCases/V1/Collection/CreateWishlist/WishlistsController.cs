using Microsoft.AspNetCore.Mvc;

namespace TreniniDotNet.Web.UseCases.V1.Collection.CreateWishlist
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public sealed class WishlistsController : ControllerBase
    {
    }
}
