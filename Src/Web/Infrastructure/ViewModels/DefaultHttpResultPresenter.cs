using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs.Validation;
using TreniniDotNet.Common.UseCases.Boundaries.Outputs;
using TreniniDotNet.Common.UseCases.Boundaries.Outputs.Ports;

namespace TreniniDotNet.Web.Infrastructure.ViewModels
{
    public abstract class DefaultHttpResultPresenter<TOutput> : IStandardOutputPort<TOutput>, IActionResultPresenter
        where TOutput : IUseCaseOutput
    {
        private static readonly IActionResult NotFoundResult = new NotFoundResult();

        public IActionResult ViewModel { get; protected set; } = NotFoundResult;

        public void InvalidRequest(IEnumerable<ValidationError> validationErrors)
        {
            var modelState = new ModelStateDictionary();

            foreach (var error in validationErrors)
            {
                modelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }

            ViewModel = new BadRequestObjectResult(modelState);
        }

        public void Error(string? message)
        {
            throw new ApplicationException(message);
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