﻿using NodaTime;
using System;
using System.Collections.Immutable;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Uuid;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Domain.Collection.Wishlists
{
    public sealed class WishlistsFactory : IWishlistsFactory
    {
        private readonly IClock _clock;
        private readonly IGuidSource _guidSource;

        public WishlistsFactory(IClock clock, IGuidSource guidSource)
        {
            _clock = clock ??
                throw new ArgumentNullException(nameof(clock));

            _guidSource = guidSource ??
                throw new ArgumentNullException(nameof(guidSource));
        }

        public IWishList NewWishlist(Owner owner, Slug slug, string? listName, Visibility visibility)
        {
            return new WishList(
                new WishlistId(_guidSource.NewGuid()),
                owner,
                slug,
                listName,
                visibility,
                ImmutableList<IWishlistItem>.Empty,
                _clock.GetCurrentInstant(),
                null,
                1);
        }

        public IWishlistItem NewWishlistItem()
        {
            throw new NotImplementedException();
        }
    }
}
