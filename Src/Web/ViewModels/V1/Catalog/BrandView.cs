using System;
using System.Text.Json.Serialization;
using TreniniDotNet.Common.Addresses;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Web.ViewModels.Links;

namespace TreniniDotNet.Web.ViewModels.V1.Catalog
{
    public class BrandView
    {
        public BrandView(IBrand b, LinksView? selfLink)
        {
            Links = selfLink;

            Id = b.BrandId.ToGuid();
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

    public class AddressView
    {
        internal AddressView(Address address)
        {
            Line1 = address.Line1;
            Line2 = address.Line2;
            City = address.City;
            Region = address.Region;
            PostalCode = address.PostalCode;
            Country = address.Country;
        }

        public string Line1 { get; }
        public string? Line2 { get; }
        public string City { get; }
        public string? Region { get; }
        public string PostalCode { get; }
        public string Country { get; }
    }
}
