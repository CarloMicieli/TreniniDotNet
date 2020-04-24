using MediatR;
using System;

namespace TreniniDotNet.Web.UseCases.V1.Collection.RemoveItemFromWishlist
{
    public sealed class RemoveItemFromWishlistRequest : IRequest
    {
        public Guid Id { set; get; }
        public Guid ItemId { set; get; }
        public string? Owner { set; get; }
    }
}
