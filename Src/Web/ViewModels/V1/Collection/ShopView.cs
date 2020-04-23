using System;
using TreniniDotNet.Domain.Collection.Shops;

namespace TreniniDotNet.Web.ViewModels.V1.Collection
{
    public sealed class ShopView
    {
        private readonly IShop _shop;

        public ShopView(IShop shop)
        {
            _shop = shop;
        }


        public Guid ShopId => _shop.ShopId.ToGuid();

        public string Slug => _shop.Slug.Value;

        public string Name => _shop.Name;

        public string? WebsiteUrl => _shop.WebsiteUrl?.ToString();

        public string? EmailAddress => _shop.EmailAddress?.ToString();

        //Address? Address { get; }

        public string? PhoneNumber => _shop.PhoneNumber?.ToString();
    }
}