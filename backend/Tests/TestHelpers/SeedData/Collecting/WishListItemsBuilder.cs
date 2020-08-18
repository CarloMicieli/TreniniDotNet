using System;
using NodaTime;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Wishlists;

namespace TreniniDotNet.TestHelpers.SeedData.Collecting
{
    public sealed class WishListItemsBuilder
    {
        private WishlistItemId _itemId;
        private CatalogItemRef _catalogItem;
        private Priority _priority;
        private LocalDate _addedDate;
        private LocalDate? _removedDate;
        private Price _price;
        private string _notes;

        internal WishListItemsBuilder()
        {
            _itemId = WishlistItemId.NewId();
        }

        public WishlistItem Build()
        {
            return new WishlistItem(
                _itemId,
                _catalogItem,
                _priority,
                _addedDate,
                _removedDate,
                _price,
                _notes);
        }

        public WishListItemsBuilder ItemId(Guid id)
        {
            _itemId = new WishlistItemId(id);
            return this;
        }

        public WishListItemsBuilder CatalogItem(CatalogItem catalogItem)
        {
            _catalogItem = new CatalogItemRef(catalogItem);
            return this;
        }

        public WishListItemsBuilder Priority(Priority priority)
        {
            _priority = priority;
            return this;
        }

        public WishListItemsBuilder Price(Price price)
        {
            _price = price;
            return this;
        }

        public WishListItemsBuilder AddedDate(LocalDate addedDate)
        {
            _addedDate = addedDate;
            return this;
        }

        public WishListItemsBuilder RemovedDate(LocalDate removedDate)
        {
            _removedDate = removedDate;
            return this;
        }

        public WishListItemsBuilder Notes(string notes)
        {
            _notes = notes;
            return this;
        }
    }
}