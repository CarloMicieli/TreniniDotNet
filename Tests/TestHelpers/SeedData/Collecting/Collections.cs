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
        public CollectionsBuilder New() => new CollectionsBuilder();

        public Collection GeorgeCollection { get; }
        public Collection RocketCollection { get; }

        internal Collections()
        {
            #region [ Init dataset ]

            GeorgeCollection = NewGeorgeCollection();
            RocketCollection = NewRocketCollection();

            #endregion
        }

        public IEnumerable<Collection> All()
        {
            yield return GeorgeCollection;
            yield return RocketCollection;
        }

        public List<Collection> NewList() =>
            new List<Collection>()
            {
                NewGeorgeCollection(),
                NewRocketCollection()
            };

        public Collection NewGeorgeCollection() => New()
            .Id(new Guid("21322602-f65a-4946-aa1c-17b66afb08c9"))
            .Owner(new Owner("George"))
            .Item(ib => ib
                .ItemId(new Guid("e7a6cd1d-cab6-4c67-be95-91f87e37ae86"))
                .CatalogItem(CatalogSeedData.CatalogItems.Acme60458)
                .Condition(Condition.New)
                .Price(Price.Euro(450))
                .AddedDate(new LocalDate(2019, 11, 25))
                .Build())
            .Build();

        public Collection NewRocketCollection() => New()
            .Id(new Guid("df165d64-ef47-48d6-ad20-75117edb914d"))
            .Owner(new Owner("Rocket"))
            .Item(ib => ib
                .ItemId(new Guid("71f6ef8f-6534-4710-973f-d3b2f606a906"))
                .CatalogItem(CatalogSeedData.CatalogItems.Acme60458)
                .Condition(Condition.New)
                .Price(Price.Euro(450))
                .AddedDate(new LocalDate(2019, 11, 25))
                .Build())
            .Item(ib => ib
                .ItemId(new Guid("ce32bd8e-7348-4ba1-9326-9451d671e27c"))
                .CatalogItem(CatalogSeedData.CatalogItems.Roco71934)
                .Condition(Condition.New)
                .Price(Price.Euro(449.90M))
                .Shop(CollectingSeedData.Shops.TecnomodelTreni)
                .AddedDate(new LocalDate(2019, 11, 25))
                .Build())
            .Item(ib => ib
                .ItemId(new Guid("8aed5d45-9ca3-47b8-b371-53c60f3b337b"))
                .CatalogItem(CatalogSeedData.CatalogItems.Acme60392)
                .Condition(Condition.New)
                .Price(Price.Euro(205M))
                .Shop(CollectingSeedData.Shops.TecnomodelTreni)
                .AddedDate(new LocalDate(2020, 11, 25))
                .Build())
            .Build();
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
