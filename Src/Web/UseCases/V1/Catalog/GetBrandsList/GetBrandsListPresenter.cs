using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TreniniDotNet.Application.Boundaries.Catalog.GetBrandsList;
using TreniniDotNet.Web.ViewModels;
using TreniniDotNet.Web.ViewModels.V1.Catalog;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.GetBrandsList
{
    public sealed class GetBrandsListPresenter : DefaultHttpResultPresenter<GetBrandsListOutput>, IGetBrandsListOutputPort
    {
        public override void Standard(GetBrandsListOutput output)
        {
            var brandsViewModel = output.Result
                .Select(brand => new BrandView(brand))
                .ToList();

            ViewModel = new OkObjectResult(brandsViewModel);
        }
    }
}
