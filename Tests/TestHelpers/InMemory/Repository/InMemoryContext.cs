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
using TreniniDotNet.TestHelpers.SeedData.Collecting;

namespace TreniniDotNet.TestHelpers.InMemory.Repository
{
    public sealed class InMemoryContext
    {
        public List<Brand> Brands { private set; get; } = new List<Brand>();

        public List<Scale> Scales { private set; get; } = new List<Scale>();

        public List<Railway> Railways { private set; get; } = new List<Railway>();

        public List<CatalogItem> CatalogItems { private set; get; } = new List<CatalogItem>();

        public List<Collection> Collections { private set; get; } = new List<Collection>();

        public List<Wishlist> WishLists { private set; get; } = new List<Wishlist>();

        public List<Shop> Shops { private set; get; } = new List<Shop>();

        public List<ShopFavourite> ShopFavourites { private set; get; } = new List<ShopFavourite>();

        public static InMemoryContext WithSeedData()
        {
            return new InMemoryContext
            {
                Brands = CatalogSeedData.Brands.All().ToList(),
                Railways = CatalogSeedData.Railways.All().ToList(),
                Scales = CatalogSeedData.Scales.All().ToList(),
                CatalogItems = CatalogSeedData.CatalogItems.All().ToList(),
                Collections = CollectingSeedData.Collections.All().ToList(),
                Shops = CollectingSeedData.Shops.All().ToList(),
                WishLists = CollectingSeedData.Wishlists.All().ToList(),
                ShopFavourites = CollectingSeedData.ShopsFavourites.All().ToList()
            };
        }
    }
}
