using System;
using TreniniDotNet.Common.Interfaces;

namespace TreniniDotNet.Application.Boundaries.Collection.DeleteWishlist
{
    public sealed class DeleteWishlistInput : IUseCaseInput
    {
        public DeleteWishlistInput(string owner, Guid id)
        {
            Id = id;
            Owner = owner;
        }

        public Guid Id { get; }
        public string Owner { get; }
    }
}
