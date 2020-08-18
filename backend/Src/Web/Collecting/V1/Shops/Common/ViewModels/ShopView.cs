using System;
using System.Text.Json.Serialization;
using TreniniDotNet.Domain.Collecting.Shops;
using TreniniDotNet.Web.Infrastructure.ViewModels.Links;

namespace TreniniDotNet.Web.Collecting.V1.Shops.Common.ViewModels
{
    public sealed class ShopView
    {
        private readonly Shop _shop;
        private readonly LinksView? _selfLink;

        public ShopView(Shop shop, LinksView? selfLink)
        {
            _shop = shop;
            _selfLink = selfLink;

            if (shop.Address?.IsEmpty == false)
            {
                Address = new Catalog.V1.Brands.Common.ViewModels.AddressView(shop.Address);
            }
        }

        [JsonPropertyName("_links")]
        public LinksView? Links => _selfLink;

        public Guid ShopId => _shop.Id;

        public string Name => _shop.Name;

        public string? WebsiteUrl => _shop.WebsiteUrl?.ToString();

        public string? EmailAddress => _shop.EmailAddress?.ToString();

        public Catalog.V1.Brands.Common.ViewModels.AddressView? Address { get; }

        public string? PhoneNumber => _shop.PhoneNumber?.ToString();
    }
}