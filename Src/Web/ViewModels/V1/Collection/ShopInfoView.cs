using System;
using TreniniDotNet.Domain.Collection.Shops;

namespace TreniniDotNet.Web.ViewModels.V1.Collection
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
