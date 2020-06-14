using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Web.Catalog.V1.Brands.Common.ViewModels;
using TreniniDotNet.Web.Catalog.V1.Scales.Common.ViewModels;
using TreniniDotNet.Web.Infrastructure.ViewModels.Links;

namespace TreniniDotNet.Web.Catalog.V1.CatalogItems.Common.ViewModels
{
    public sealed class CatalogItemView
    {
        private readonly ICatalogItem _item;

        public CatalogItemView(ICatalogItem item, LinksView? selfLink)
        {
            Links = selfLink;

            _item = item ??
                throw new ArgumentNullException(nameof(item));

            Brand = new BrandInfoView(item.Brand);
            Scale = new ScaleInfoView(item.Scale);
        }

        [JsonPropertyName("_links")]
        public LinksView? Links { set; get; }

        public Guid Id => _item.Id.ToGuid();

        public string ItemNumber => _item.ItemNumber.ToString();

        public BrandInfoView Brand { get; }

        public string Description => _item.Description;

        public string? PrototypeDescription => _item.PrototypeDescription;

        public string? ModelDescription => _item.ModelDescription;

        public string? DeliveryDate => _item.DeliveryDate?.ToString();

        public bool Available => _item.IsAvailable;

        public ScaleInfoView Scale { get; }

        public string PowerMethod => _item.PowerMethod.ToString();

        public IEnumerable<RollingStockView> RollingStocks =>
            _item.RollingStocks.Select(it => new RollingStockView(it));
    }
}