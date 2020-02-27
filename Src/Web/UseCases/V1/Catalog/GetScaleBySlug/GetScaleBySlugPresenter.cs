using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Boundaries.Catalog.GetScaleBySlug;
using TreniniDotNet.Web.ViewModels;
using TreniniDotNet.Web.ViewModels.Links;
using TreniniDotNet.Web.ViewModels.V1.Catalog;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.GetScaleBySlug
{
    public class GetScaleBySlugPresenter : DefaultHttpResultPresenter<GetScaleBySlugOutput>, IGetScaleBySlugOutputPort
    {
        private readonly ILinksGenerator _linksGenerator;

        public GetScaleBySlugPresenter(ILinksGenerator linksGenerator)
        {
            _linksGenerator = linksGenerator;
        }

        public void ScaleNotFound(string message)
        {
            ViewModel = new NotFoundResult();
        }

        public override void Standard(GetScaleBySlugOutput output)
        {
            var selfLink = _linksGenerator.GenerateSelfLink(
                nameof(ScalesController.GetScaleBySlug), 
                output.Scale.Slug);

            var scaleViewModel = new ScaleView(output.Scale, selfLink);
            ViewModel = new OkObjectResult(scaleViewModel);
        }
    }
}