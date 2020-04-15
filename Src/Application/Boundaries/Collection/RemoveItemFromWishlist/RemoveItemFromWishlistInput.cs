using System;
using TreniniDotNet.Common.Interfaces;

namespace TreniniDotNet.Application.Boundaries.Collection.RemoveItemFromWishlist
{
    public sealed class RemoveItemFromWishlistInput : IUseCaseInput
    {
        public RemoveItemFromWishlistInput(Guid id, Guid itemId)
        {
            Id = id;
            ItemId = itemId;
        }

        public Guid Id { get; }
        public Guid ItemId { get; }
    }
}
