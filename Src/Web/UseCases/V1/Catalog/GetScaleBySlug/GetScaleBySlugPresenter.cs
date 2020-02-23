using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Boundaries.Catalog.GetScaleBySlug;
using TreniniDotNet.Web.ViewModels;
using TreniniDotNet.Web.ViewModels.V1.Catalog;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.GetScaleBySlug
{
    public class GetScaleBySlugPresenter : DefaultHttpResultPresenter<GetScaleBySlugOutput>, IGetScaleBySlugOutputPort
    {
        public void ScaleNotFound(string message)
        {
            ViewModel = new NotFoundResult();
        }

        public override void Standard(GetScaleBySlugOutput output)
        {
            var scaleView = new ScaleView(output.Scale);
            ViewModel = new OkObjectResult(scaleView);
        }
    }
}