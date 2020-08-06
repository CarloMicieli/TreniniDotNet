using System;
using MediatR;
using Newtonsoft.Json;
using TreniniDotNet.Web.Collecting.V1.Common.Requests;

namespace TreniniDotNet.Web.Collecting.V1.Wishlists.EditWishlistItem
{
    public sealed class EditWishlistItemRequest : IRequest
    {
        [JsonIgnore]
        public Guid Id { set; get; }
        [JsonIgnore]
        public Guid ItemId { set; get; }
        [JsonIgnore]
        public string? Owner { set; get; }
        public DateTime? AddedDate { get; }
        public PriceRequest? Price { get; }
        public string? Priority { get; }
        public string? Notes { get; }
    }
}
