using System;
using MediatR;

namespace TreniniDotNet.Web.Collecting.V1.Wishlists.GetWishlistById
{
    public sealed class GetWishlistByIdRequest : IRequest
    {
        public Guid Id { get; set; }
        public string? Owner { get; set; }
    }
}
