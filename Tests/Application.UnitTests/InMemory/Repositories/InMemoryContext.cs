using System.Collections.Generic;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.TestHelpers.SeedData.Catalog;

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

        public ICollection<ICatalogItem> CatalogItems { set; get; } = new List<ICatalogItem>();

        public static InMemoryContext WithCatalogSeedData()
        {
            return new InMemoryContext
            {
                Brands = CatalogSeedData.Brands.All(),
                Railways = CatalogSeedData.Railways.All(),
                Scales = CatalogSeedData.Scales.All(),
                CatalogItems = CatalogSeedData.CatalogItems.All()
            };
        }
    }
}