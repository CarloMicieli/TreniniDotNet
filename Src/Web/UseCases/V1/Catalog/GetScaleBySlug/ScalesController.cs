using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Common;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.GetScaleBySlug
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ScalesController : UseCaseController<GetScaleBySlugRequest, GetScaleBySlugPresenter>
    {
        public ScalesController(IMediator mediator, GetScaleBySlugPresenter presenter)
            : base(mediator, presenter)
        {
        }

        [HttpGet]
        [Route("{slug}", Name = nameof(GetScaleBySlug))]
        public Task<IActionResult> GetBrandBySlug(string slug)
        {
            return HandleRequest(new GetScaleBySlugRequest(Slug.Of(slug)));
        }
    }
}