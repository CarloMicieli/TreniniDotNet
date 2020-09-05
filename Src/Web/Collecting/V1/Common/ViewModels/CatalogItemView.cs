using System;
using TreniniDotNet.Domain.Collecting.Shared;

namespace TreniniDotNet.Web.Collecting.V1.Common.ViewModels
{
    public sealed class CatalogItemView
    {
        private readonly CatalogItemRef _catalogItem;

        public CatalogItemView(CatalogItemRef catalogItem)
        {
            _catalogItem = catalogItem;
        }

        public Guid CatalogItemId => _catalogItem.Id;

        public string Slug => _catalogItem.Slug;

        public string BrandName => _catalogItem.BrandName;

        public string ItemNumber => _catalogItem.ItemNumber.Value;

        public string Category => _catalogItem.Category.ToString();

        public string Description => _catalogItem.ToString();
    }
}
