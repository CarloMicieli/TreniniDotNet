using System;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;

namespace TreniniDotNet.Application.Collecting.Wishlists.GetWishlistById
{
    public sealed class GetWishlistByIdInput : IUseCaseInput
    {
        public GetWishlistByIdInput(string owner, Guid id)
        {
            Id = id;
            Owner = owner;
        }

        public Guid Id { get; }
        public string Owner { get; }
    }
}
