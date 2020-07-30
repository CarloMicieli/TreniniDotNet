using System;
using TreniniDotNet.Domain.Collecting.Shops;

namespace TreniniDotNet.Web.Collecting.V1.Shops.Common.ViewModels
{
    public sealed class ShopView
    {
        private readonly Shop _shop;

        public ShopView(Shop shop)
        {
            _shop = shop;

            if (!(shop.Address is null))
            {
                Address = new Catalog.V1.Brands.Common.ViewModels.AddressView(shop.Address);
            }
        }

        public Guid ShopId => _shop.Id;

        public string Slug => _shop.Slug.Value;

        public string Name => _shop.Name;

        public string? WebsiteUrl => _shop.WebsiteUrl?.ToString();

        public string? EmailAddress => _shop.EmailAddress?.ToString();

        public Catalog.V1.Brands.Common.ViewModels.AddressView? Address { get; }

        public string? PhoneNumber => _shop.PhoneNumber?.ToString();
    }
}