using MediatR;
using Newtonsoft.Json;
using System;

namespace TreniniDotNet.Web.UseCases.V1.Collection.EditCollectionItem
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

        public decimal? Price { get; set; }

        public string? Condition { get; set; }

        public DateTime? AddedDate { get; set; }

        public string? Notes { get; set; }
    }
}
