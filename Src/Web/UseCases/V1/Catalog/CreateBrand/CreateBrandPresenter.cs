using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TreniniDotNet.Application.Boundaries.Catalog.CreateBrand;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.CreateBrand
{
    public class CreateBrandPresenter : ICreateBrandOutputPort
    {
        public IActionResult ViewModel { get; private set; } = new NotFoundResult();

        public void BrandAlreadyExists(string message)
        {
            ViewModel = new BadRequestObjectResult(message);
        }

        public void InvalidRequest(List<ValidationFailure> failures)
        {
            throw new System.NotImplementedException();
        }

        public void Standard(CreateBrandOutput output)
        {
            ViewModel = new CreatedAtRouteResult(
                "GetBrand",
                new
                {
                    slug = output.Slug,
                    version = "1.0",
                },
                output);
        }
    }
}
