using System;
using MediatR;
using Newtonsoft.Json;
using TreniniDotNet.Web.Collecting.V1.Common.Requests;

namespace TreniniDotNet.Web.Collecting.V1.Collections.AddItemToCollection
{
    public sealed class AddItemToCollectionRequest : IRequest
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        [JsonIgnore]
        public string? Owner { get; set; }

        public string? CatalogItem { get; set; }

        public string? Shop { get; set; }

        public PriceRequest? Price { get; set; }

        public string? Condition { get; set; }

        public DateTime AddedDate { get; set; }

        public string? Notes { get; set; }
    }
}
