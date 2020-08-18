using System;
using MediatR;
using Newtonsoft.Json;

namespace TreniniDotNet.Web.Collecting.V1.Wishlists.DeleteWishlist
{
    public sealed class DeleteWishlistRequest : IRequest
    {
        [JsonIgnore]
        public string? Owner { get; set; }
        public Guid Id { get; set; }
    }
}
