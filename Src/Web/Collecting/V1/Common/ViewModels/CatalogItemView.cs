using System;
using System.Text.Json.Serialization;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Web.Infrastructure.ViewModels.Links;

namespace TreniniDotNet.Web.Collecting.V1.Common.ViewModels
{
    public sealed class CatalogItemView
    {
        private readonly CatalogItemRef _catalogItem;

        public CatalogItemView(CatalogItemRef catalogItem)
        {
            //Links = selfLink;
            this._catalogItem = catalogItem;
        }

        // [JsonPropertyName("_links")]
        // public LinksView? Links { get; }

        public Guid CatalogItemId => _catalogItem.Id;

        public string Slug => _catalogItem.Slug;
    }
}
