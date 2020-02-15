using System.Text.Json.Serialization;
using TreniniDotNet.Domain.Catalog.Brands;

namespace TreniniDotNet.Web.ViewModels.V1.Catalog
{
    public class BrandView
    {
        public BrandView(IBrand b)
        {
            Id = b.BrandId.ToString();
            Slug = b.Slug.ToString();
            Name = b.Name;
            CompanyName = b.CompanyName;
            MailAddress = b.EmailAddress?.ToString();
            WebsiteUrl = b.WebsiteUrl?.ToString();
            Kind = b.Kind.ToString();
        }

        public string Id { set; get; } = null!;
        public string Slug { set; get; } = null!;
        public string Name { set; get; } = null!;

        [JsonPropertyName("company_name")]
        public string? CompanyName { set; get; }

        [JsonPropertyName("mail_address")]
        public string? MailAddress { set; get; }

        [JsonPropertyName("website_url")]
        public string? WebsiteUrl { set; get; }
        public string? Kind { set; get; }
    }
}
