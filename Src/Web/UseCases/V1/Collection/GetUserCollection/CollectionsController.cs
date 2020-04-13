﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Collection.GetUserCollection;

namespace TreniniDotNet.Web.UseCases.V1.Collection.GetUserCollection
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public sealed class CollectionsController : UseCaseController<GetUserCollectionRequest, GetUserCollectionPresenter>
    {
        public CollectionsController(IMediator mediator, GetUserCollectionPresenter presenter)
            : base(mediator, presenter)
        {
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetUserCollectionOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> Get(GetUserCollectionRequest request)
        {
            return HandleRequest(request);
        }
    }
}
