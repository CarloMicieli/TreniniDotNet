using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Collecting.Collections.EditCollectionItem;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Collecting.V1.Collections.EditCollectionItem
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public sealed class CollectionsController : UseCaseController<EditCollectionItemRequest, EditCollectionItemPresenter>
    {
        public CollectionsController(IMediator mediator, EditCollectionItemPresenter presenter)
            : base(mediator, presenter)
        {
        }

        [HttpPut("{id}/items/{itemId}")]
        [ProducesResponseType(typeof(EditCollectionItemOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> EditCollectionItem(Guid id, Guid itemId, EditCollectionItemRequest request)
        {
            request.Id = id;
            request.ItemId = itemId;
            request.Owner = CurrentUser;

            return HandleRequest(request);
        }
    }
}
