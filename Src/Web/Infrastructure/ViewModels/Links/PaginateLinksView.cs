using System.Text.Json.Serialization;

namespace TreniniDotNet.Web.Infrastructure.ViewModels.Links
{
    public sealed class PaginateLinksView
    {
        [JsonPropertyName("_self")]
        public string Self { set; get; } = "";
        public string? Prev { set; get; }
        public string? Next { set; get; }
    }
}
