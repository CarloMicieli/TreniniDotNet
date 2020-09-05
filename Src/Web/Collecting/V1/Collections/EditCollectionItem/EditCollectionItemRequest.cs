using System;
using System.Text.Json.Serialization;
using MediatR;
using TreniniDotNet.Web.Collecting.V1.Common.Requests;

namespace TreniniDotNet.Web.Collecting.V1.Collections.EditCollectionItem
{
    public sealed class EditCollectionItemRequest : IRequest
    {
        [JsonIgnore]
        public string? Owner { get; set; }

        [JsonIgnore]
        public Guid Id { get; set; }

        [JsonIgnore]
        public Guid ItemId { get; set; }

        public string? Shop { get; set; }

        public PriceRequest? Price { get; set; }

        public string? Condition { get; set; }

        public DateTime? AddedDate { get; set; }

        public string? Notes { get; set; }
    }
}
