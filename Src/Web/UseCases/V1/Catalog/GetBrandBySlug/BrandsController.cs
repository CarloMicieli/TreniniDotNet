using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Web.ViewModels.V1.Catalog;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.GetBrandBySlug
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly GetBrandBySlugPresenter _presenter;

        public BrandsController(IMediator mediator, GetBrandBySlugPresenter presenter)
        {
            _mediator = mediator;
            _presenter = presenter;
        }

        [HttpGet]
        [Route("{slug}", Name = nameof(GetBrandBySlug))]
        public async Task<ActionResult<BrandView>> GetBrandBySlug(string slug)
        {
            await _mediator.Send(new GetBrandBySlugRequest(Slug.Of(slug)));
            return _presenter.ViewModel;
        }
    }
}