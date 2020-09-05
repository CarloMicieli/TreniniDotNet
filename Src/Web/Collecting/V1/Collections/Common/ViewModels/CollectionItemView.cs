using System;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Web.Collecting.V1.Common.ViewModels;
using TreniniDotNet.Web.Collecting.V1.Shops.Common.ViewModels;

namespace TreniniDotNet.Web.Collecting.V1.Collections.Common.ViewModels
{
    public sealed class CollectionItemView
    {
        private readonly CollectionItem _inner;

        public CollectionItemView(CollectionItem it)
        {
            _inner = it;
            CatalogItem = new CatalogItemView(it.CatalogItem);

            if (!(it.PurchasedAt is null))
            {
                PurchasedAt = new ShopInfoView(it.PurchasedAt);
            }
        }

        public Guid ItemId => _inner.Id;

        public CatalogItemView CatalogItem { get; }

        public string Condition => _inner.Condition.ToString();

        public PriceView Price => new PriceView(_inner.Price);

        public ShopInfoView? PurchasedAt { get; }

        public DateTime AddedDate => _inner.AddedDate.ToDateTimeUnspecified();

        public string? Notes => _inner.Notes;
    }
}