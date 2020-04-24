﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Collection.GetFavouriteShops;

namespace TreniniDotNet.Web.UseCases.V1.Collection.GetFavouriteShops
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public sealed class ShopsController : UseCaseController<GetFavouriteShopsRequest, GetFavouriteShopsPresenter>
    {
        public ShopsController(IMediator mediator, GetFavouriteShopsPresenter presenter)
            : base(mediator, presenter)
        {
        }

        [HttpGet("favourites")]
        [ProducesResponseType(typeof(GetFavouriteShopsOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> Get(GetFavouriteShopsRequest request)
        {
            return HandleRequest(request);
        }
    }
}
