using System.Text.Json.Serialization;

namespace TreniniDotNet.Web.ViewModels.Links
{
    public class LinksView
    {
        [JsonPropertyName("_self")]
        public string Self { set; get; } = "";
    }
}
