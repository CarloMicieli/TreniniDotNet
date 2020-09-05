using System;
using System.Text.Json.Serialization;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Web.Infrastructure.ViewModels.Links;

namespace TreniniDotNet.Web.Catalog.V1.Brands.Common.ViewModels
{
    public class BrandView
    {
        public BrandView(Brand b, LinksView? selfLink)
        {
            Links = selfLink;

            Id = b.Id;
            Name = b.Name;
            CompanyName = b.CompanyName;
            MailAddress = b.EmailAddress?.ToString();
            WebsiteUrl = b.WebsiteUrl?.ToString();
            Kind = b.Kind.ToString();

            if (!(b.Address is null))
            {
                Address = new AddressView(b.Address);
            }
        }

        [JsonPropertyName("_links")]
        public LinksView? Links { set; get; }
        public Guid Id { set; get; }
        public string Name { set; get; } = null!;
        public string? CompanyName { set; get; }
        public string? MailAddress { set; get; }
        public string? WebsiteUrl { set; get; }
        public string? Kind { set; get; }
        public AddressView? Address { set; get; }
    }
}
