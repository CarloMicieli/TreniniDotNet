using System.Collections.Immutable;
using System.Linq;
using NodaTime;
using TreniniDotNet.Domain.Collecting.Shared;

namespace TreniniDotNet.Domain.Collecting.Collections
{
    public sealed class CollectionStats
    {
        internal CollectionStats(
            CollectionId id,
            Owner owner,
            Instant modifiedDate,
            Price totalValue,
            IImmutableList<CollectionStatsItem> categories)
        {
            Id = id;
            Owner = owner;
            ModifiedDate = modifiedDate;
            TotalValue = totalValue;
            CategoriesByYear = categories;
        }

        public static CollectionStats FromCollection(Collection collection)
        {
            var items = collection.Items
                .Select(it => new
                {
                    Count = it.CatalogItem.Count,
                    Category = it.CatalogItem.Category,
                    Year = Year.FromLocalDate(it.AddedDate),
                    it.Price
                })
                .GroupBy(it => new { it.Category, it.Year })
                .Select(it => new CollectionStatsItem(
                    it.Key.Year,
                    it.Key.Category,
                    it.Sum(y => y.Count),
                    it.Select(y => y.Price).Sum()))
                .ToImmutableList();

            var totalValue = items.Select(it => it.TotalValue).Sum();

            return new CollectionStats(
                collection.Id,
                collection.Owner,
                collection.ModifiedDate ?? collection.CreatedDate,
                totalValue,
                items);
        }

        public CollectionId Id { get; }

        public Owner Owner { get; }

        public Instant ModifiedDate { get; }

        public Price TotalValue { get; }

        public IImmutableList<CollectionStatsItem> CategoriesByYear { get; }
    }
}
