using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;
using TreniniDotNet.Domain.Pagination;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.GetBrandsList
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]    
    public class BrandsController : UseCaseController<GetBrandsListRequest, GetBrandsListPresenter>
    {
        public BrandsController(IMediator mediator, GetBrandsListPresenter presenter) 
            : base(mediator, presenter)
        {
        }

        [HttpGet]
        public Task<IActionResult> GetBrands(int start = 0, int limit = 50)
        {
            return HandleRequest(new GetBrandsListRequest(new Page(start, limit)));
        }
    }
}