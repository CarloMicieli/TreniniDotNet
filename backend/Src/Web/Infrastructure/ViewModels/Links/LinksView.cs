using System.Text.Json.Serialization;

namespace TreniniDotNet.Web.Infrastructure.ViewModels.Links
{
    public class LinksView
    {
        [JsonPropertyName("_self")]
        public string Self { set; get; } = "";

        public string Slug { set; get; } = "";
    }
}
