using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NodaTime;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.TestHelpers.SeedData.Catalog;

namespace TreniniDotNet.TestHelpers.SeedData.Collecting
{
    public sealed class Collections
    {
        private readonly Collection _georgeCollection;
        private readonly Collection _rocketCollection;
        private readonly IList<Collection> _all;

        public CollectionsBuilder New() => new CollectionsBuilder();

        internal Collections()
        {
            #region [ Init data ]
            _georgeCollection = New()
                .Id(new Guid("21322602-f65a-4946-aa1c-17b66afb08c9"))
                .Owner(new Owner("George"))
                .Item(ib => ib
                    .CatalogItem(CatalogSeedData.CatalogItems.Acme_60458())
                    .Condition(Condition.New)
                    .Price(Price.Euro(450))
                    .AddedDate(new LocalDate(2019, 11, 25))
                    .Build())
                .Build();

            _rocketCollection = New()
                .Id(new Guid("df165d64-ef47-48d6-ad20-75117edb914d"))
                .Owner(new Owner("Rocket"))
                .Item(ib => ib
                    .CatalogItem(CatalogSeedData.CatalogItems.Acme_60458())
                    .Condition(Condition.New)
                    .Price(Price.Euro(450))
                    .AddedDate(new LocalDate(2019, 11, 25))
                    .Build())
                .Item(ib => ib
                    .CatalogItem(CatalogSeedData.CatalogItems.Roco_71934())
                    .Condition(Condition.New)
                    .Price(Price.Euro(449.90M))
                    // .Shop(CollectionSeedData.Shops.TecnomodelTreni())
                    .AddedDate(new LocalDate(2019, 11, 25))
                    .Build())
                .Item(ib => ib
                    .CatalogItem(CatalogSeedData.CatalogItems.Acme_60392())
                    .Condition(Condition.New)
                    .Price(Price.Euro(205M))
                    // .Shop(CollectionSeedData.Shops.TecnomodelTreni())
                    .AddedDate(new LocalDate(2020, 11, 25))
                    .Build())
                .Build();
            #endregion

            _all = new List<Collection>()
            {
                _georgeCollection,
                _rocketCollection
            };
        }

        public IList<Collection> All() => _all;

        public Collection GeorgeCollection() => _georgeCollection;

        public Collection RocketCollection() => _rocketCollection;
    }

    public static class CollectionsRepositoryExtensions
    {
        public static async Task SeedDatabase(this ICollectionsRepository repo)
        {
            var collections = CollectingSeedData.Collections.All();
            foreach (var c in collections)
            {
                await repo.AddAsync(c);
            }
        }
    }
}
