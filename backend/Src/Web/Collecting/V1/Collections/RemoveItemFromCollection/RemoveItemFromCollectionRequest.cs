using System;
using MediatR;
using Newtonsoft.Json;

namespace TreniniDotNet.Web.Collecting.V1.Collections.RemoveItemFromCollection
{
    public sealed class RemoveItemFromCollectionRequest : IRequest
    {
        [JsonIgnore]
        public string? Owner { get; set; }
        public Guid Id { get; set; }
        public Guid ItemId { get; set; }
        public DateTime? Removed { get; set; }
    }
}
