using System;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;

namespace TreniniDotNet.Application.Collecting.Wishlists.RemoveItemFromWishlist
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
