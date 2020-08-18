using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.Web.Catalog.V1.Scales.Common.ViewModels;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Catalog.V1.Scales.GetScaleBySlug
{
    [AllowAnonymous]
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
        [ProducesResponseType(typeof(ScaleView), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> GetScaleBySlug(string slug)
        {
            return HandleRequest(new GetScaleBySlugRequest(Slug.Of(slug)));
        }
    }
}