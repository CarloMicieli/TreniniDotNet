using MediatR;
using Newtonsoft.Json;
using System;

namespace TreniniDotNet.Web.UseCases.V1.Collection.EditWishlistItem
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
        public decimal? Price { get; }
        public string? Priority { get; }
        public string? Notes { get; }
    }
}
