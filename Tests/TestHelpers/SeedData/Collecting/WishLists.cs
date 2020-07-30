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
        private readonly Wishlist _georgeList1;
        private readonly Wishlist _rocketList1;

        private readonly IList<Wishlist> _all;

        public WishListsBuilder New() => new WishListsBuilder();

        internal WishLists()
        {
            _georgeList1 = New()
                .Id(new Guid("a3d61748-a85e-41e8-a904-f205c99495a3"))
                .Owner(new Owner("George"))
                .ListName("First list")
                .Visibility(Visibility.Private)
                .Item(ib => ib
                    .CatalogItem(CatalogSeedData.CatalogItems.Acme_60392())
                    .Priority(Priority.Normal)
                    .AddedDate(new LocalDate(2019, 11, 25))
                    .Build())
                .Build();

            _rocketList1 = New()
                .Id(WishlistId.NewId())
                .Owner(new Owner("Rocket"))
                .ListName("First list")
                .Visibility(Visibility.Public)
                .Item(ib => ib
                    .CatalogItem(CatalogSeedData.CatalogItems.Acme_60392())
                    .Priority(Priority.Normal)
                    .AddedDate(new LocalDate(2019, 11, 25))
                    .Build())
                .Build();

            _all = new List<Wishlist>()
            {
                _georgeList1,
                _rocketList1
            };
        }

        public IList<Wishlist> All() => _all;

        public Wishlist GeorgeFirstList() => _georgeList1;
        public Wishlist RocketFirstList() => _rocketList1;
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
