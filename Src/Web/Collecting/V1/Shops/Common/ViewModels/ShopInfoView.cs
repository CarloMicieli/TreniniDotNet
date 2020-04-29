using System;
using TreniniDotNet.Domain.Collecting.Shops;

namespace TreniniDotNet.Web.Collecting.V1.Shops.Common.ViewModels
{
    public sealed class ShopInfoView
    {
        private readonly IShopInfo _shopInfo;

        public ShopInfoView(IShopInfo shopInfo)
        {
            _shopInfo = shopInfo;
        }

        public Guid ShopId => _shopInfo.ShopId.ToGuid();

        public string Slug => _shopInfo.Slug.Value;

        public string Name => _shopInfo.Name;
    }
}
