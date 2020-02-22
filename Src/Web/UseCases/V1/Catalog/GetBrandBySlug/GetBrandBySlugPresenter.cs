using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TreniniDotNet.Application.Boundaries.Catalog.GetBrandBySlug;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Web.ViewModels.V1.Catalog;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.GetBrandBySlug
{
    public class GetBrandBySlugPresenter : IOutputPort
    {
        public ActionResult<BrandView> ViewModel { get; private set; } = null!;

        public void BrandNotFound(string message)
        {
            ViewModel = new NotFoundResult();
        }

        public void Error(string? message)
        {
            throw new System.NotImplementedException();
        }

        public void InvalidRequest(List<ValidationFailure> failures)
        {
            throw new System.NotImplementedException();
        }

        public void Standard(GetBrandBySlugOutput output)
        {
            if (output.Brand is null)
            {
                ViewModel = new NotFoundResult();
            }
            else
            {
                var brandViewModel = new BrandView(output.Brand);
                ViewModel = new ActionResult<BrandView>(brandViewModel);
            }
        }
    }
}
