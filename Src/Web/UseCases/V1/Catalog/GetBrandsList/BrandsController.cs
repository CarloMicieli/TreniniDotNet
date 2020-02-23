using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public Task<IActionResult> GetBrands()
        {
            return HandleRequest(new GetBrandsListRequest());
        }
    }
}