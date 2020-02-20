using System.Collections.Generic;
using TreniniDotNet.Application.SeedData.Catalog;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.Scales;

namespace TreniniDotNet.Application.InMemory
{
    public sealed class InMemoryContext
    {
        public InMemoryContext()
        {
            this.Brands = new List<IBrand>();
        }

        public ICollection<IBrand> Brands { set; get; }

        public ICollection<Scale> Scales { set; get; }

        public ICollection<Railway> Railways { set; get; }

        public ICollection<CatalogItem> CatalogItems { set; get; }

        public static InMemoryContext WithCatalogSeedData()
        {
            return new InMemoryContext
            {
                Brands = CatalogSeedData.Brands,
                Railways = CatalogSeedData.Railways,
                Scales = CatalogSeedData.Scales
            };
        }
    }
}