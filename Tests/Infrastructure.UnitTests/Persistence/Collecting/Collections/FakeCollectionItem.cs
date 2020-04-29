using System;
using NodaMoney;
using NodaTime;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Shops;
using TreniniDotNet.Domain.Collecting.ValueObjects;

namespace TreniniDotNet.Infrastructure.Persistence.Collecting.Collections
{
    internal class FakeCollectionItem : ICollectionItem
    {
        public CollectionItemId ItemId { set; get; }

        public ICatalogRef CatalogItem { set; get; }

        public ICatalogItemDetails Details => null;

        public Condition Condition { set; get; }

        public Money Price { set; get; }

        public IShopInfo PurchasedAt { set; get; }

        public LocalDate AddedDate { set; get; }

        public string Notes { set; get; }

        public FakeCollectionItem()
        {
            ItemId = new CollectionItemId(new Guid("eff28064-28fc-402f-aa63-c6ea5099cc22"));
            CatalogItem = CatalogRef.Of(new Guid("bce9490c-7050-444d-9127-0c2ac52fb068"), "acme-123456");
            Condition = Condition.New;
            Price = Money.Euro(150);
            AddedDate = new LocalDate(2019, 11, 25);
            Notes = "My test notes";
            PurchasedAt = new ShopInfo(new ShopId(new Guid("4c2fa45c-5c8b-4b6e-860c-994fb032ebfb")), "test shop");
        }

        public ICollectionItem With(
            Condition? Condition = null,
            Money? Price = null,
            IShopInfo PurchasedAt = null,
            LocalDate? AddedDate = null,
            string Notes = null)
        {
            return new FakeCollectionItem
            {
                ItemId = this.ItemId,
                CatalogItem = this.CatalogItem,
                Condition = Condition ?? this.Condition,
                Price = Price ?? this.Price,
                AddedDate = AddedDate ?? this.AddedDate,
                PurchasedAt = (PurchasedAt is null) ? this.PurchasedAt : PurchasedAt,
                Notes = (Notes is null) ? null : Notes
            };
        }
    }
}
