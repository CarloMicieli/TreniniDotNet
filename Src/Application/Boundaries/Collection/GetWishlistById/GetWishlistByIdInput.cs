using System;
using TreniniDotNet.Common.Interfaces;

namespace TreniniDotNet.Application.Boundaries.Collection.GetWishlistById
{
    public sealed class GetWishlistByIdInput : IUseCaseInput
    {
        public GetWishlistByIdInput(Guid wishlistId)
        {
            WishlistId = wishlistId;
        }

        public Guid WishlistId { get; }
    }
}
