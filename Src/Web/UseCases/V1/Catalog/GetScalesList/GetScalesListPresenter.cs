using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Boundaries.Catalog.GetScalesList;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Web.ViewModels;
using TreniniDotNet.Web.ViewModels.Links;
using TreniniDotNet.Web.ViewModels.Pagination;
using TreniniDotNet.Web.ViewModels.V1.Catalog;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.GetScalesList
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
