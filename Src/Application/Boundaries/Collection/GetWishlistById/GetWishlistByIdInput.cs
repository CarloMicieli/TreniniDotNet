using System;
using TreniniDotNet.Common.Interfaces;

namespace TreniniDotNet.Application.Boundaries.Collection.GetWishlistById
{
    public sealed class GetWishlistByIdInput : IUseCaseInput
    {
        public GetWishlistByIdInput(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
