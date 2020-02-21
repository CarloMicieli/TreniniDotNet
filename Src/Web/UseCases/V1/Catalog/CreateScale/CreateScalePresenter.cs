using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TreniniDotNet.Application.Boundaries.Catalog.CreateScale;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.CreateScale
{
    public class CreateScalePresenter : IOutputPort
    {
        public IActionResult ViewModel { get; private set; } = new NotFoundResult();

        public void InvalidRequest(List<ValidationFailure> failures)
        {
            throw new System.NotImplementedException();
        }

        public void ScaleAlreadyExists(string message)
        {
            ViewModel = new BadRequestObjectResult(message);
        }

        public void Standard(CreateScaleOutput output)
        {
            ViewModel = new CreatedAtRouteResult(
                "GetScale",
                new
                {
                    slug = output.Slug,
                    version = "1.0",
                },
                output);
        }        
    }
}