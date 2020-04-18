using System.Collections.Generic;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Collection.Collections;
using TreniniDotNet.Domain.Collection.Shops;
using TreniniDotNet.Domain.Collection.Wishlists;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using TreniniDotNet.TestHelpers.SeedData.Collection;

namespace TreniniDotNet.Application.InMemory.Repositories
{
    public sealed class InMemoryContext
    {
        public InMemoryContext()
        {
        }

        public IList<IBrand> Brands { set; get; } = new List<IBrand>();

        public IList<IScale> Scales { set; get; } = new List<IScale>();

        public IList<IRailway> Railways { set; get; } = new List<IRailway>();

        public IList<ICatalogItem> CatalogItems { set; get; } = new List<ICatalogItem>();

        public IList<ICollection> Collections { set; get; } = new List<ICollection>();

        public IList<IWishlist> WishLists { set; get; } = new List<IWishlist>();

        public IList<IShop> Shops { set; get; } = new List<IShop>();

        public static InMemoryContext WithSeedData()
        {
            return new InMemoryContext
            {
                Brands = CatalogSeedData.Brands.All(),
                Railways = CatalogSeedData.Railways.All(),
                Scales = CatalogSeedData.Scales.All(),
                CatalogItems = CatalogSeedData.CatalogItems.All(),
                Collections = CollectionSeedData.Collections.All(),
                Shops = CollectionSeedData.Shops.All(),
                WishLists = CollectionSeedData.Wishlists.All()
            };
        }
    }
}