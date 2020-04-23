using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries;
using TreniniDotNet.Web.ViewModels;

namespace TreniniDotNet.Web.UseCases
{
    public abstract class UseCaseController<TRequest, TPresenter> : ControllerBase
        where TRequest : IRequest
        where TPresenter : IActionResultPresenter, IOutputPortErrors
    {
        private readonly IMediator _mediator;
        private readonly TPresenter _presenter;

        protected UseCaseController(IMediator mediator, TPresenter presenter)
        {
            _mediator = mediator ??
                throw new ArgumentNullException(nameof(mediator));
            _presenter = presenter ??
                throw new ArgumentNullException(nameof(presenter));
        }

        protected async Task<IActionResult> HandleRequest(TRequest request)
        {
            await _mediator.Send(request);
            return _presenter.ViewModel;
        }

        protected string? CurrentUser => HttpContext.User.Identity.Name;
    }
}
