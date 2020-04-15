using System;
using TreniniDotNet.Common.Interfaces;

namespace TreniniDotNet.Application.Boundaries.Collection.DeleteWishlist
{
    public sealed class DeleteWishlistInput : IUseCaseInput
    {
        public DeleteWishlistInput(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
