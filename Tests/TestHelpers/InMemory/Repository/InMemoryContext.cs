using System.Collections.Generic;
using System.Linq;
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
        public List<IBrand> Brands { private set; get; } = new List<IBrand>();

        public List<IScale> Scales { private set; get; } = new List<IScale>();

        public List<IRailway> Railways { private set; get; } = new List<IRailway>();

        public List<ICatalogItem> CatalogItems { private set; get; } = new List<ICatalogItem>();

        public List<ICollection> Collections { private set; get; } = new List<ICollection>();

        public List<IWishlist> WishLists { private set; get; } = new List<IWishlist>();

        public List<IShop> Shops { private set; get; } = new List<IShop>();

        public static InMemoryContext WithSeedData()
        {
            return new InMemoryContext
            {
                Brands = CatalogSeedData.Brands.All().ToList(),
                Railways = CatalogSeedData.Railways.All().ToList(),
                Scales = CatalogSeedData.Scales.All().ToList(),
                CatalogItems = CatalogSeedData.CatalogItems.All().ToList(),
                Collections = CollectionSeedData.Collections.All().ToList(),
                Shops = CollectionSeedData.Shops.All().ToList(),
                WishLists = CollectionSeedData.Wishlists.All().ToList()
            };
        }
    }
}
