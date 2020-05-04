using System;
using System.Text.Json.Serialization;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Web.Infrastructure.ViewModels.Links;

namespace TreniniDotNet.Web.Collecting.V1.Common.ViewModels
{
    public sealed class CatalogItemView
    {
        private readonly ICatalogRef _catalogItem;

        public CatalogItemView(ICatalogRef catalogItem)
        {
            //Links = selfLink;
            this._catalogItem = catalogItem;
        }

        // [JsonPropertyName("_links")]
        // public LinksView? Links { get; }

        public Guid CatalogItemId => _catalogItem.CatalogItemId.ToGuid();

        public string Slug => _catalogItem.Slug.Value;
    }
}
