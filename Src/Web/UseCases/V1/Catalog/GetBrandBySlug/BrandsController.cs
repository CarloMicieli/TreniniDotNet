using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TreniniDotNet.Common;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.GetBrandBySlug
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BrandsController : UseCaseController<GetBrandBySlugRequest, GetBrandBySlugPresenter>
    {
        public BrandsController(IMediator mediator, GetBrandBySlugPresenter presenter)
            : base(mediator, presenter)
        {
        }

        [HttpGet]
        [Route("{slug}", Name = nameof(GetBrandBySlug))]
        public Task<IActionResult> GetBrandBySlug(string slug)
        {
            return HandleRequest(new GetBrandBySlugRequest(Slug.Of(slug)));
        }
    }
}