using MediatR;
using System.Text.Json.Serialization;

namespace TreniniDotNet.Web.UseCases.V1.Collection.CreateCollection
{
    public sealed class CreateCollectionRequest : IRequest
    {
        [JsonIgnore]
        public string? Owner { get; set; }

        public string? Notes { get; set; }
    }
}
