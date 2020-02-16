using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Common;
using TreniniDotNet.Web.ViewModels.V1.Catalog;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.GetScaleBySlug
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ScalesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly GetScaleBySlugPresenter _presenter;

        public ScalesController(IMediator mediator, GetScaleBySlugPresenter presenter)
        {
            _mediator = mediator;
            _presenter = presenter;
        }

        [HttpGet]
        [Route("{slug}")]
        public async Task<ActionResult<ScaleView>> GetBrandBySlug(string slug)
        {
            await _mediator.Send(new GetScaleBySlugRequest(Slug.Of(slug)));
            return _presenter.ViewModel;
        }
    }
}