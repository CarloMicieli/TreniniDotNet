using System.Collections.Generic;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Domain.Collecting.Shops;
using TreniniDotNet.Domain.Collecting.Wishlists;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using TreniniDotNet.TestHelpers.SeedData.Collection;

namespace TreniniDotNet.TestHelpers.InMemory.Repository
{
    public sealed class InMemoryContext
    {
        public IList<IBrand> Brands { private set; get; } = new List<IBrand>();

        public IList<IScale> Scales { private set; get; } = new List<IScale>();

        public IList<IRailway> Railways { private set; get; } = new List<IRailway>();

        public IList<ICatalogItem> CatalogItems { private set; get; } = new List<ICatalogItem>();

        public IList<ICollection> Collections { private set; get; } = new List<ICollection>();

        public IList<IWishlist> WishLists { private set; get; } = new List<IWishlist>();

        public IList<IShop> Shops { private set; get; } = new List<IShop>();

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
