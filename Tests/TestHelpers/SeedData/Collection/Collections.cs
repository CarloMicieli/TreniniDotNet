using NodaMoney;
using NodaTime;
using NodaTime.Testing;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using TreniniDotNet.Common.Uuid;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Collection.Collections;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Domain.Collection.ValueObjects;
using TreniniDotNet.TestHelpers.SeedData.Catalog;

namespace TreniniDotNet.TestHelpers.SeedData.Collection
{
    public sealed class Collections
    {
        private static ICollectionsFactory factory = new CollectionsFactory(
            FakeClock.FromUtc(1988, 11, 25),
            new GuidSource());

        private readonly ICollection _collection;
        private readonly IList<ICollection> _all;

        internal Collections()
        {
            _collection = NewWith(
                new Guid("21322602-f65a-4946-aa1c-17b66afb08c9"),
                "George",
                new List<ICollectionItem>()
                {
                    NewItem(CatalogSeedData.CatalogItems.Acme_60458())
                });

            _all = new List<ICollection>()
            {
                _collection
            };
        }

        public IList<ICollection> All() => _all;

        public ICollection GeorgeCollection() => _collection;

        private static ICollection NewWith(Guid id, string owner, IEnumerable<ICollectionItem> items)
        {
            return factory.NewCollection(id, owner, items.ToImmutableList(), DateTime.UtcNow, null, 1);
        }

        private static ICollectionItem NewItem(ICatalogItem catalogItem)
        {
            var catalogRef = CatalogRef.From(catalogItem);
            var details = CatalogItemDetails.FromCatalogItem(catalogItem);

            return factory.NewCollectionItem(
                catalogRef,
                details,
                Condition.New,
                Money.Euro(450),
                new LocalDate(2019, 11, 25),
                null,
                null);
        }
    }

    public static class ICollectionsRepositoryExtensions
    {
        public static async Task SeedDatabase(this ICollectionsRepository repo, ICollectionItemsRepository itemsRepo)
        {
            var collections = CollectionSeedData.Collections.All();
            foreach (var c in collections)
            {
                var id = await repo.AddAsync(c);

                foreach (var item in c.Items)
                {
                    await itemsRepo.AddItemAsync(id, item);
                }
            }
        }
    }
}
