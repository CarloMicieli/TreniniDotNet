using System;
using System.Collections.Generic;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TreniniDotNet.Common.UseCases.Interfaces.Output;

namespace TreniniDotNet.Web.Infrastructure.ViewModels
{
    /// <summary>
    /// An helper class to produce HTTP results from a presenters
    /// </summary>
    public abstract class DefaultHttpResultPresenter<TOutput> : IOutputPortStandard<TOutput>, IActionResultPresenter
        where TOutput : IUseCaseOutput
    {
        public IActionResult ViewModel { get; protected set; } = new NotFoundResult();

        public void Error(string? message)
        {
            throw new ApplicationException(message);
        }

        public void InvalidRequest(IList<ValidationFailure> failures)
        {
            var modelState = new ModelStateDictionary();

            foreach (var error in failures)
            {
                string key = error.PropertyName;
                modelState.AddModelError(key, error.ErrorMessage);
            }

            ViewModel = new BadRequestObjectResult(modelState);
        }

        public abstract void Standard(TOutput output);

        protected IActionResult BadRequest(string message)
        {
            return new BadRequestObjectResult(message);
        }

        protected IActionResult Created(string routeName, object routeParams, TOutput output)
        {
            return new CreatedAtRouteResult(
                 routeName,
                 routeParams,
                 output);
        }
    }
}
