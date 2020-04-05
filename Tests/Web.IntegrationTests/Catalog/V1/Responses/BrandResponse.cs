using System;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.Responses
{
    internal class AddressResponse
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }

    internal class BrandResponse
    {
        public SelfLinks _Links { set; get; }
        public Guid Id { set; get; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }
        public string MailAddress { get; set; }
        public string WebsiteUrl { get; set; }
        public string Kind { get; set; }
        public bool? Active { get; set; }
        public AddressResponse Address { get; set; }
    }
}