using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NodaTime;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Wishlists;
using TreniniDotNet.TestHelpers.SeedData.Catalog;

namespace TreniniDotNet.TestHelpers.SeedData.Collecting
{
    public sealed class WishLists
    {
        public WishListsBuilder New() => new WishListsBuilder();

        public IEnumerable<Wishlist> All()
        {
            yield return GeorgeFirstList();
            yield return RocketFirstList();
        }

        public Wishlist GeorgeFirstList() => New()
            .Id(new Guid("a3d61748-a85e-41e8-a904-f205c99495a3"))
            .Owner(new Owner("George"))
            .ListName("First list")
            .Visibility(Visibility.Private)
            .Item(ib => ib
                .ItemId(new Guid("2f9020b0-c97e-4ac1-9c71-00724ccb424b"))
                .CatalogItem(CatalogSeedData.CatalogItems.Acme_60392())
                .Priority(Priority.Normal)
                .AddedDate(new LocalDate(2019, 11, 25))
                .Build())
            .Build();
        
        public Wishlist RocketFirstList() => New()
            .Id(new Guid("69624d68-6c65-4c0e-9382-176b8a23eee9"))
            .Owner(new Owner("Rocket"))
            .ListName("First list")
            .Visibility(Visibility.Public)
            .Item(ib => ib
                .ItemId(new Guid("4f3ffa76-2a4f-46a6-8fd4-6c1b18b6c49c"))
                .CatalogItem(CatalogSeedData.CatalogItems.Acme_60392())
                .Priority(Priority.Normal)
                .AddedDate(new LocalDate(2019, 11, 25))
                .Build())
            .Build();
    }

    public static class WishlistsRepositoryExtensions
    {
        public static async Task SeedDatabase(this IWishlistsRepository repo)
        {
            var wishlists = CollectingSeedData.Wishlists.All();
            foreach (var wl in wishlists)
            {
                await repo.AddAsync(wl);
            }
        }
    }
}
