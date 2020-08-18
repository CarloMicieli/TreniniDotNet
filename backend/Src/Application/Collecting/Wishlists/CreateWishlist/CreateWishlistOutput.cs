using System;
using TreniniDotNet.Common.UseCases.Boundaries.Outputs;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Wishlists;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Collecting.Wishlists.CreateWishlist
{
    public sealed class CreateWishlistOutput : IUseCaseOutput
    {
        public CreateWishlistOutput(WishlistId wishlistId, Owner owner, Slug slug)
        {
            WishlistId = wishlistId.ToGuid();
            Slug = slug;
            Owner = owner.Value;
        }

        public Guid WishlistId { get; }
        public string Owner { get; }
        public Slug Slug { get; }
    }
}
