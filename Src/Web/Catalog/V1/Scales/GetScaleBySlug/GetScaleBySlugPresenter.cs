using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Catalog.Scales.GetScaleBySlug;
using TreniniDotNet.Web.Catalog.V1.Scales.Common.ViewModels;
using TreniniDotNet.Web.Infrastructure.ViewModels;
using TreniniDotNet.Web.Infrastructure.ViewModels.Links;

namespace TreniniDotNet.Web.Catalog.V1.Scales.GetScaleBySlug
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