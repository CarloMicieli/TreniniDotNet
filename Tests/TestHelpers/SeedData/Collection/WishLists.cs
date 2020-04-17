using NodaTime.Testing;
using System.Collections.Generic;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Uuid;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Domain.Collection.ValueObjects;
using TreniniDotNet.Domain.Collection.Wishlists;

namespace TreniniDotNet.TestHelpers.SeedData.Collection
{
    public sealed class WishLists
    {
        private static IWishlistsFactory factory = new WishlistsFactory(
            FakeClock.FromUtc(1988, 11, 25),
            new GuidSource());

        private readonly IWishList _george_list1;

        private readonly IList<IWishList> _all;

        internal WishLists()
        {
            _george_list1 = NewWishlist("George", "First list", Visibility.Private);

            _all = new List<IWishList>()
            {
                _george_list1
            };
        }

        public IList<IWishList> All() => _all;


        private static IWishList NewWishlist(string owner, string listTitle, Visibility visibility)
        {
            return factory.NewWishlist(new Owner(owner), Slug.Of(listTitle), listTitle, visibility);
        }
    }
}