using System;
using System.Collections.Generic;
using NodaTime;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Wishlists;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.TestHelpers.SeedData.Collecting
{
    public sealed class WishListsBuilder
    {
        private WishlistId _wishlistId;
        private Owner _owner;
        private string _listName;
        private Visibility _visibility;
        private Budget _budget;
        private List<WishlistItem> _items;
        private Instant _createdDate;
        private Instant? _modifiedDate;
        private int _version;

        internal WishListsBuilder()
        {
            _wishlistId = WishlistId.NewId();
            _items = new List<WishlistItem>();
            _createdDate = Instant.FromDateTimeUtc(DateTime.UtcNow);
            _modifiedDate = null;
            _budget = null;
            _listName = null;
            _version = 1;
        }

        public WishListsBuilder Id(Guid id)
        {
            _wishlistId = new WishlistId(id);
            return this;
        }

        public WishListsBuilder Owner(Owner owner)
        {
            _owner = owner;
            return this;
        }

        public WishListsBuilder Budget(Budget budget)
        {
            _budget = budget;
            return this;
        }


        public WishListsBuilder ListName(string listName)
        {
            _listName = listName;
            return this;
        }

        public WishListsBuilder Visibility(Visibility visibility)
        {
            _visibility = visibility;
            return this;
        }

        public WishListsBuilder Item(Action<WishListItemsBuilder> actionBuild)
        {
            var wishItemBuilder = new WishListItemsBuilder();
            actionBuild(wishItemBuilder);
            _items.Add(wishItemBuilder.Build());
            return this;
        }

        public Wishlist Build()
        {
            return new Wishlist(
                _wishlistId,
                _owner,
                _listName,
                _visibility,
                _budget,
                _items,
                _createdDate,
                _modifiedDate,
                _version);
        }
    }
}
