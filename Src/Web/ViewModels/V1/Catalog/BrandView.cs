using System.Text.Json.Serialization;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Web.ViewModels.Links;

namespace TreniniDotNet.Web.ViewModels.V1.Catalog
{
    public class BrandView
    {
        public BrandView(IBrand b, LinksView? selfLink)
        {
            Links = selfLink;
            Id = b.BrandId.ToString();
            Name = b.Name;
            CompanyName = b.CompanyName;
            MailAddress = b.EmailAddress?.ToString();
            WebsiteUrl = b.WebsiteUrl?.ToString();
            Kind = b.Kind.ToString();
        }


        [JsonPropertyName("_links")]
        public LinksView? Links { set; get; }
        public string Id { set; get; } = null!;
        public string Name { set; get; } = null!;
        public string? CompanyName { set; get; }
        public string? MailAddress { set; get; }
        public string? WebsiteUrl { set; get; }
        public string? Kind { set; get; }
    }
}
