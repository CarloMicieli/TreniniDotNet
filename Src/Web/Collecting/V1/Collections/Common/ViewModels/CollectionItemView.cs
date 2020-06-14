using System;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Web.Collecting.V1.Common.ViewModels;
using TreniniDotNet.Web.Collecting.V1.Shops.Common.ViewModels;

namespace TreniniDotNet.Web.Collecting.V1.Collections.Common.ViewModels
{
    public sealed class CollectionItemView
    {
        private readonly ICollectionItem _inner;

        public CollectionItemView(ICollectionItem it)
        {
            _inner = it;
            CatalogItem = new CatalogItemView(it.CatalogItem);

            if (it.Details != null)
            {
                Details = new CatalogItemDetailsView(it.Details);
            }

            if (it.PurchasedAt != null)
            {
                PurchasedAt = new ShopInfoView(it.PurchasedAt);
            }
        }

        public Guid ItemId => _inner.Id.ToGuid();

        public CatalogItemView CatalogItem { get; }

        public CatalogItemDetailsView? Details { get; }

        public string Condition => _inner.Condition.ToString();

        public MoneyView Price => new MoneyView(_inner.Price);

        public ShopInfoView? PurchasedAt { get; }

        public DateTime AddedDate => _inner.AddedDate.ToDateTimeUnspecified();

        public string? Notes => _inner.Notes;
    }
}