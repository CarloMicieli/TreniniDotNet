using System;
using TreniniDotNet.Domain.Collecting.Collections;

namespace TreniniDotNet.Web.Collecting.V1.Shops.Common.ViewModels
{
    public sealed class ShopInfoView
    {
        private readonly ShopRef _shop;

        public ShopInfoView(ShopRef shop)
        {
            _shop = shop;
        }

        public Guid ShopId => _shop.Id;

        public string Slug => _shop.Slug;

        public string Name => _shop.ToString();
    }
}
