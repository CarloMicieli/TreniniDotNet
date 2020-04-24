using MediatR;
using Newtonsoft.Json;
using System;

namespace TreniniDotNet.Web.UseCases.V1.Collection.AddItemToWishlist
{
    public sealed class AddItemToWishlistRequest : IRequest
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        [JsonIgnore]
        public string? Owner { get; set; }
        public string? CatalogItem { get; set; }
        public DateTime AddedDate { get; set; }
        public decimal? Price { get; set; }
        public string? Priority { get; set; }
        public string? Notes { get; set; }
    }
}
