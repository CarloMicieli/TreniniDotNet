using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Boundaries.Collection.GetShopsList;
using TreniniDotNet.Web.ViewModels;

namespace TreniniDotNet.Web.UseCases.V1.Collection.GetShopsList
{
    public class GetShopsListPresenter : DefaultHttpResultPresenter<GetShopsListOutput>, IGetShopsListOutputPort
    {
        public override void Standard(GetShopsListOutput output)
        {
            ViewModel = new OkObjectResult(output.Shops);
        }
    }
}