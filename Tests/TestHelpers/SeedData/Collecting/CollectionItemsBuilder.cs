using System;
using NodaTime;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Shops;

namespace TreniniDotNet.TestHelpers.SeedData.Collecting
{
    public sealed class CollectionItemsBuilder
    {
        private CollectionItemId _itemId;
        private CatalogItem _catalogItem;
        private Condition _condition;
        private Price _price;
        private Shop _purchasedAt;
        private LocalDate _addedDate;
        private LocalDate? _removedDate;
        private string _notes;

        internal CollectionItemsBuilder()
        {
            _itemId = CollectionItemId.NewId();
            _condition = Domain.Collecting.Collections.Condition.New;
        }

        public CollectionItemsBuilder ItemId(Guid id)
        {
            _itemId = new CollectionItemId(id);
            return this;
        }

        public CollectionItemsBuilder CatalogItem(CatalogItem catalogItem)
        {
            _catalogItem = catalogItem;
            return this;
        }

        public CollectionItemsBuilder Condition(Condition condition)
        {
            _condition = condition;
            return this;
        }

        public CollectionItemsBuilder Price(Price price)
        {
            _price = price;
            return this;
        }

        public CollectionItemsBuilder Shop(Shop purchasedAt)
        {
            _purchasedAt = purchasedAt;
            return this;
        }

        public CollectionItemsBuilder AddedDate(LocalDate addedDate)
        {
            _addedDate = addedDate;
            return this;
        }

        public CollectionItemsBuilder RemovedDate(LocalDate removedDate)
        {
            _removedDate = removedDate;
            return this;
        }

        public CollectionItemsBuilder Notes(string notes)
        {
            _notes = notes;
            return this;
        }

        public CollectionItem Build()
        {
            return new CollectionItem(
                _itemId,
                _catalogItem,
                _condition,
                _price,
                _purchasedAt,
                _addedDate,
                _removedDate,
                _notes);
        }
    }
}
