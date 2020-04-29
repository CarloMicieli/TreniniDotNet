using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Collecting.Shops.GetShopsList;
using TreniniDotNet.Web.Infrastructure.ViewModels;

namespace TreniniDotNet.Web.Collecting.V1.Shops.GetShopsList
{
    public class GetShopsListPresenter : DefaultHttpResultPresenter<GetShopsListOutput>, IGetShopsListOutputPort
    {
        public override void Standard(GetShopsListOutput output)
        {
            ViewModel = new OkObjectResult(output.Shops);
        }
    }
}