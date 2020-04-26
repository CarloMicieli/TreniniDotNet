using MediatR;
using Newtonsoft.Json;
using System;

namespace TreniniDotNet.Web.UseCases.V1.Collection.RemoveItemFromCollection
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
