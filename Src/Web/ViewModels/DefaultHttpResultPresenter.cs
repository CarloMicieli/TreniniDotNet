using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using TreniniDotNet.Application.Boundaries;

namespace TreniniDotNet.Web.ViewModels
{
    /// <summary>
    /// An helper class to produce HTTP results from a presenters
    /// </summary>
    public abstract class DefaultHttpResultPresenter<TOutput> : IOutputPortStandard<TOutput>
        where TOutput : IUseCaseOutput
    {
        private static readonly IActionResult NotFoundResult = new NotFoundResult();

        public IActionResult ViewModel { get; protected set; } = NotFoundResult;

        public void Error(string? message)
        {
            throw new System.NotImplementedException("TODO");
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

        protected IActionResult InternalServerError(string? message)
        {
            //return StatusCode(StatusCodes.Status500InternalServerError, message);
            throw new NotImplementedException("TODO");
        }
    }
}
