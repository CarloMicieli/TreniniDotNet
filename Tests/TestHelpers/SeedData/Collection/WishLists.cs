using NodaTime;
using NodaTime.Testing;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Uuid;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.ValueObjects;
using TreniniDotNet.Domain.Collecting.Wishlists;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using static TreniniDotNet.TestHelpers.SeedData.ListHelpers;

namespace TreniniDotNet.TestHelpers.SeedData.Collection
{
    public sealed class WishLists
    {
        private static IWishlistsFactory factory = new WishlistsFactory(
            FakeClock.FromUtc(1988, 11, 25),
            new GuidSource());

        private readonly IWishlist _george_list1;

        private readonly IList<IWishlist> _all;

        internal WishLists()
        {
            _george_list1 = NewWishlist(
                id: new Guid("a3d61748-a85e-41e8-a904-f205c99495a3"),
                owner: "George",
                listTitle: "First list",
                visibility: Visibility.Private,
                ListOf(Item(CatalogRef.From(CatalogSeedData.CatalogItems.Acme_60392()))));

            _all = new List<IWishlist>()
            {
                _george_list1
            };
        }

        public IList<IWishlist> All() => _all;

        public IWishlist George_First_List() => _george_list1;

        private static IWishlist NewWishlist(Guid id, string owner, string listTitle, Visibility visibility, IEnumerable<IWishlistItem> items)
        {
            return factory.NewWishlist(
                new WishlistId(id),
                new Owner(owner),
                Slug.Of(listTitle),
                listTitle,
                visibility,
                items.ToImmutableList(),
                Instant.FromUtc(2019, 11, 25, 9, 0),
                null,
                1);
        }

        private static IWishlistItem Item(ICatalogRef catalogRef)
        {
            return factory.NewWishlistItem(catalogRef, Priority.Normal, new LocalDate(2019, 11, 25), null, null);
        }
    }

    public static class IWishlistsRepositoryExtensions
    {
        public static async Task SeedDatabase(this IWishlistsRepository repo, IWishlistItemsRepository itemsRepo)
        {
            var wishlists = CollectionSeedData.Wishlists.All();
            foreach (var wl in wishlists)
            {
                var id = await repo.AddAsync(wl);

                foreach (var item in wl.Items)
                {
                    await itemsRepo.AddItemAsync(id, item);
                }
            }
        }
    }
}