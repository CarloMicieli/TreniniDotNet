using System;
using TreniniDotNet.Common.Interfaces;

namespace TreniniDotNet.Application.Boundaries.Collection.RemoveItemFromWishlist
{
    public sealed class RemoveItemFromWishlistInput : IUseCaseInput
    {
        public RemoveItemFromWishlistInput(string owner, Guid id, Guid itemId)
        {
            Owner = owner;
            Id = id;
            ItemId = itemId;
        }

        public string Owner { get; }
        public Guid Id { get; }
        public Guid ItemId { get; }
    }
}
