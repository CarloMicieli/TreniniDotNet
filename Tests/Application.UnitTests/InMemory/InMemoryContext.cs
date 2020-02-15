using System.Collections.Generic;
using TreniniDotNet.Application.SeedData.Catalog;
using TreniniDotNet.Domain.Catalog.Brands;

namespace TreniniDotNet.Application.InMemory
{
    public sealed class InMemoryContext
    {
        public InMemoryContext()
        {
            this.Brands = new List<IBrand>();
        }

        public ICollection<IBrand> Brands { set; get; }

        public static InMemoryContext WithCatalogSeedData()
        {
            return new InMemoryContext
            {
                Brands = CatalogSeedData.Brands
            };
        }
    }
}