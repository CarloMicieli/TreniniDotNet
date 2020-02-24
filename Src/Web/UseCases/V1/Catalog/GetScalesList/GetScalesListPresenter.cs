using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TreniniDotNet.Application.Boundaries.Catalog.GetScalesList;
using TreniniDotNet.Web.ViewModels;
using TreniniDotNet.Web.ViewModels.V1.Catalog;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.GetScalesList
{
    public class GetScalesListPresenter : DefaultHttpResultPresenter<GetScalesListOutput>, IGetScalesListOutputPort
    {
        public override void Standard(GetScalesListOutput output)
        {
            var scalesViewModel = output.Result
                .Select(brand => new ScaleView(brand))
                .ToList();

            ViewModel = new OkObjectResult(scalesViewModel);
        }
    }
}
