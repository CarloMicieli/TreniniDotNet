using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using NodaTime;
using TreniniDotNet.Common.Domain;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Domain.Collecting.Wishlists
{
    public sealed class Wishlist : AggregateRoot<WishlistId>
    {
        private Wishlist()
        {
        }

        public Wishlist(
            WishlistId wishlistId,
            Owner owner,
            string? listName,
            Visibility visibility,
            Budget? budget,
            IEnumerable<WishlistItem> items,
            Instant createdDate,
            Instant? modifiedDate,
            int version)
            : base(wishlistId, createdDate, modifiedDate, version)
        {
            Owner = owner;
            Budget = budget;
            ListName = listName;
            Visibility = visibility;
            _items = items.ToList();
            Slug = BuildSlug(owner, listName);
        }

        #region [ Properties ]
        public Owner Owner { get; }

        public Budget? Budget { get; }

        private readonly List<WishlistItem> _items = new List<WishlistItem>();
        public IReadOnlyCollection<WishlistItem> Items => ItemsEn()
            .ToImmutableList();

        public Slug Slug { get; }

        public string? ListName { get; }

        public Visibility Visibility { get; }
        #endregion

        public int Count => ItemsEn().Count();

        public void AddItem(WishlistItem item)
        {
            _items.Add(item);
        }

        public void UpdateItem(WishlistItem item)
        {
            _items.RemoveAll(it => it.Id == item.Id);
            _items.Add(item);
        }

        public void RemoveItem(WishlistItemId itemId, LocalDate removeDate)
        {
            var item = _items.FirstOrDefault(it => it.Id == itemId);
            if (item == null)
            {
                return;
            }

            _items.Remove(item);
            _items.Add(item.With(removedDate: removeDate));

            var count = _items.Where(it => it.RemovedDate.HasValue == false);
        }

        public bool Contains(CatalogItem catalogItem) =>
            _items.Any(it => it.CatalogItem == catalogItem);

        public WishlistItem? FindItemById(WishlistItemId itemId) =>
            _items.FirstOrDefault(it => it.Id == itemId);

        public WishlistSummary CalculateSummary() =>
            WishlistSummary.Of(this);

        public (decimal amount, string? currency) CalculateTotalValue()
        {
            var totalValue = _items.Select(it => it.Price?.Amount).Sum();
            var currencyCode = _items.Where(it => !(it.Price is null))
                .Select(it => it.Price?.Currency)
                .FirstOrDefault();

            return (totalValue ?? 0, currencyCode);
        }

        public Wishlist With(
            string? listName = null,
            Visibility? visibility = null,
            Budget? budget = null)
        {
            return new Wishlist(
                Id,
                Owner,
                listName ?? ListName,
                visibility ?? Visibility,
                budget ?? Budget,
                _items,
                CreatedDate,
                ModifiedDate,
                Version);
        }

        private static Slug BuildSlug(Owner owner, string? listName)
        {
            if (string.IsNullOrEmpty(listName))
            {
                return SharedKernel.Slugs.Slug.Of(owner.Value);
            }
            else
            {
                return Slug.Of(owner.Value, listName ?? "");
            }
        }

        private IEnumerable<WishlistItem> ItemsEn() =>
            _items.Where(it => it.RemovedDate.HasValue == false);

        public override string ToString() => $"Wishlist({Id}, {Owner}, {ListName})";
    }
}
