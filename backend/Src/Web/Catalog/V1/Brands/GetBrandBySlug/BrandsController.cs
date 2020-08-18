using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.Web.Catalog.V1.Brands.Common.ViewModels;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Catalog.V1.Brands.GetBrandBySlug
{
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public sealed class BrandsController : UseCaseController<GetBrandBySlugRequest, GetBrandBySlugPresenter>
    {
        public BrandsController(IMediator mediator, GetBrandBySlugPresenter presenter)
            : base(mediator, presenter)
        {
        }

        [HttpGet]
        [Route("{slug}", Name = nameof(GetBrandBySlug))]
        [ProducesResponseType(typeof(BrandView), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> GetBrandBySlug(string slug)
        {
            return HandleRequest(new GetBrandBySlugRequest(Slug.Of(slug)));
        }
    }
}