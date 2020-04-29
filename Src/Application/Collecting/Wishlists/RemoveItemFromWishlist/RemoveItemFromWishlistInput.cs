using System;
using TreniniDotNet.Common.UseCases.Interfaces;
using TreniniDotNet.Common.UseCases.Interfaces.Input;

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
