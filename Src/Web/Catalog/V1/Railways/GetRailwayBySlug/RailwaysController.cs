using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Common;
using TreniniDotNet.Web.Catalog.V1.Railways.Common.ViewModels;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Catalog.V1.Railways.GetRailwayBySlug
{
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class RailwaysController : UseCaseController<GetRailwayBySlugRequest, GetRailwayBySlugPresenter>
    {
        public RailwaysController(IMediator mediator, GetRailwayBySlugPresenter presenter)
            : base(mediator, presenter)
        {
        }

        [HttpGet]
        [Route("{slug}", Name = nameof(GetRailwayBySlug))]
        [ProducesResponseType(typeof(RailwayView), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> GetRailwayBySlug(string slug)
        {
            return HandleRequest(new GetRailwayBySlugRequest(Slug.Of(slug)));
        }
    }
}