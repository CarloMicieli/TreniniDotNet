using System;
using TreniniDotNet.Domain.Collecting.Shared;

namespace TreniniDotNet.Web.Collecting.V1.Common.ViewModels
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
