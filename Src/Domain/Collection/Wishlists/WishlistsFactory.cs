using NodaTime;
using System;
using TreniniDotNet.Common.Uuid;

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

        public IWishList NewWishlist()
        {
            throw new NotImplementedException();
        }

        public IWishlistItem NewWishlistItem()
        {
            throw new NotImplementedException();
        }
    }
}
