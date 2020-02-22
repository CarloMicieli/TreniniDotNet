using TreniniDotNet.Application.Boundaries.Catalog.GetScaleBySlug;
using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Web.ViewModels.V1.Catalog;
using System.Collections.Generic;
using FluentValidation.Results;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.GetScaleBySlug
{
    public class GetScaleBySlugPresenter : IOutputPort
    {
        public GetScaleBySlugPresenter()
        {
        }

        public ActionResult<ScaleView> ViewModel { get; private set; } = null!;

        public void Error(string? message)
        {
            throw new System.NotImplementedException();
        }

        public void InvalidRequest(IList<ValidationFailure> failures)
        {
            throw new System.NotImplementedException();
        }

        public void ScaleNotFound(string message)
        {
            ViewModel = new NotFoundResult();
        }

        public void Standard(GetScaleBySlugOutput output)
        {
            if (output.Scale is null)
            {
                ViewModel = new NotFoundResult();
            }
            else
            {
                //var scaleViewModel = _mapper.Map<ScaleView>(output.Scale);
                ViewModel = new ActionResult<ScaleView>(new ScaleView { });
            }
        }
    }
}