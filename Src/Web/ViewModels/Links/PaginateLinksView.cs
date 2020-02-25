using System.Text.Json.Serialization;

namespace TreniniDotNet.Web.ViewModels.Links
{
    public sealed class PaginateLinksView
    {
        [JsonPropertyName("_self")]
        public string Self { set; get; } = "";
        public string? Prev { set; get; }
        public string? Next { set; get; }
    }
}
