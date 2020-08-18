using System.Text.Json.Serialization;
using MediatR;

namespace TreniniDotNet.Web.Collecting.V1.Collections.CreateCollection
{
    public sealed class CreateCollectionRequest : IRequest
    {
        [JsonIgnore]
        public string? Owner { get; set; }

        public string? Notes { get; set; }
    }
}
