using System;
using TreniniDotNet.Domain.Collection.Shared;

namespace TreniniDotNet.Web.ViewModels.V1.Collection
{
    public sealed class CatalogItemView
    {
        private readonly ICatalogRef _catalogItem;

        public CatalogItemView(ICatalogRef catalogItem)
        {
            this._catalogItem = catalogItem;
        }

        public Guid CatalogItemId => _catalogItem.CatalogItemId.ToGuid();

        public string Slug => _catalogItem.Slug.Value;
    }
}
