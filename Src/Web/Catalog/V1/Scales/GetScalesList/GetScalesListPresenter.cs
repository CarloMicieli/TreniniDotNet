using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Catalog.Scales.GetScalesList;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Web.Catalog.V1.Scales.Common.ViewModels;
using TreniniDotNet.Web.Infrastructure.ViewModels;
using TreniniDotNet.Web.Infrastructure.ViewModels.Links;
using TreniniDotNet.Web.Infrastructure.ViewModels.Pagination;

namespace TreniniDotNet.Web.Catalog.V1.Scales.GetScalesList
{
    public class GetScalesListPresenter : DefaultHttpResultPresenter<GetScalesListOutput>, IGetScalesListOutputPort
    {
        private readonly ILinksGenerator _linksGenerator;

        public GetScalesListPresenter(ILinksGenerator linksGenerator)
        {
            _linksGenerator = linksGenerator;
        }

        public override void Standard(GetScalesListOutput output)
        {
            var links = _linksGenerator.Generate(nameof(ScalesController.GetScales), output.PaginatedResult);

            PaginatedViewModel<ScaleView> viewModel = output
                .PaginatedResult
                .ToViewModel(links, ToScaleView);

            ViewModel = new OkObjectResult(viewModel);
        }

        private ScaleView ToScaleView(IScale scale)
        {
            return new ScaleView(scale, _linksGenerator.GenerateSelfLink(
                nameof(GetScaleBySlug.ScalesController.GetScaleBySlug),
                scale.Slug));
        }
    }
}
