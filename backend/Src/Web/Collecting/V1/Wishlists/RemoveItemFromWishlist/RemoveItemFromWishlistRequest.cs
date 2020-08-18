using System;
using MediatR;

namespace TreniniDotNet.Web.Collecting.V1.Wishlists.RemoveItemFromWishlist
{
    public sealed class RemoveItemFromWishlistRequest : IRequest
    {
        public Guid Id { set; get; }
        public Guid ItemId { set; get; }
        public string? Owner { set; get; }
    }
}
