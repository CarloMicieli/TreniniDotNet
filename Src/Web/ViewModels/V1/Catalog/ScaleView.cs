using System.Text.Json.Serialization;

namespace TreniniDotNet.Web.ViewModels.V1.Catalog
{
    public class ScaleView
    {
        public string Id { set; get; } = null!;
        
        public string Slug { set; get; } = null!;
        
        public string Name { set; get; } = null!;
        
        public string Ratio { set; get; } = null!;
        
        public decimal? Gauge { set; get; }

        [JsonPropertyName("is_narrow")]
        public bool? IsNarrow { set; get; }
    }
}