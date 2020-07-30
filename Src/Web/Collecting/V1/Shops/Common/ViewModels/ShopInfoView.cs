using System;
using TreniniDotNet.Domain.Collecting.Shops;

namespace TreniniDotNet.Web.Collecting.V1.Shops.Common.ViewModels
{
    public sealed class ShopInfoView
    {
        private readonly Shop _shop;

        public ShopInfoView(Shop shop)
        {
            _shop = shop;
        }

        public Guid ShopId => _shop.Id;

        public string Slug => _shop.Slug.Value;

        public string Name => _shop.Name;
    }
}
