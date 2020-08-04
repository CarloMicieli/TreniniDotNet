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
        public Wishlist GeorgeList1 { get; }
        public Wishlist RocketList1 { get; }

        public WishListsBuilder New() => new WishListsBuilder();

        internal WishLists()
        {
            #region [ Init dataset ]

            GeorgeList1 = NewGeorgeFirstList();
            RocketList1 = NewRocketFirstList();

            #endregion
        }

        public IEnumerable<Wishlist> All()
        {
            yield return GeorgeList1;
            yield return RocketList1;
        }

        public List<Wishlist> NewList() =>
            new List<Wishlist>()
            {
                NewGeorgeFirstList(),
                NewRocketFirstList()
            };

        public Wishlist NewGeorgeFirstList() => New()
            .Id(new Guid("a3d61748-a85e-41e8-a904-f205c99495a3"))
            .Owner(new Owner("George"))
            .ListName("First list")
            .Visibility(Visibility.Private)
            .Budget(new Budget(1000, "EUR"))
            .Item(ib => ib
                .ItemId(new Guid("2f9020b0-c97e-4ac1-9c71-00724ccb424b"))
                .CatalogItem(CatalogSeedData.CatalogItems.Acme60392)
                .Priority(Priority.Normal)
                .Price(Price.Euro(200))
                .AddedDate(new LocalDate(2019, 11, 25))
                .Build())
            .Build();

        public Wishlist NewRocketFirstList() => New()
            .Id(new Guid("69624d68-6c65-4c0e-9382-176b8a23eee9"))
            .Owner(new Owner("Rocket"))
            .ListName("First list")
            .Visibility(Visibility.Public)
            .Item(ib => ib
                .ItemId(new Guid("4f3ffa76-2a4f-46a6-8fd4-6c1b18b6c49c"))
                .CatalogItem(CatalogSeedData.CatalogItems.Acme60392)
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
