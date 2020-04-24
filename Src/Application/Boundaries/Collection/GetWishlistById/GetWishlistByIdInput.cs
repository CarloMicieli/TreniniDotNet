using System;
using TreniniDotNet.Common.Interfaces;

namespace TreniniDotNet.Application.Boundaries.Collection.GetWishlistById
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
