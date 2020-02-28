using System.Collections.Generic;
using TreniniDotNet.Application.SeedData.Catalog;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.Scales;

namespace TreniniDotNet.Application.InMemory.Repositories
{
    public sealed class InMemoryContext
    {
        public InMemoryContext()
        {
        }

        public ICollection<IBrand> Brands { set; get; } = new List<IBrand>();

        public ICollection<IScale> Scales { set; get; } = new List<IScale>();

        public ICollection<IRailway> Railways { set; get; } = new List<IRailway>();

        public ICollection<CatalogItem> CatalogItems { set; get; }

        public static InMemoryContext WithCatalogSeedData()
        {
            return new InMemoryContext
            {
                Brands = CatalogSeedData.Brands,
                Railways = CatalogSeedData.Railways,
                Scales = CatalogSeedData.Scales,
                CatalogItems = CatalogSeedData.CatalogItems
            };
        }
    }
}