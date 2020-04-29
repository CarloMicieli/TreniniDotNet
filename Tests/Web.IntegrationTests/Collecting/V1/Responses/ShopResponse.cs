using System;

namespace TreniniDotNet.IntegrationTests.Collecting.V1.Responses
{
    public class ShopResponse
    {
        public Guid ShopId { set; get; }

        public string Slug { set; get; }

        public string Name { set; get; }

        public string WebsiteUrl { set; get; }

        public string EmailAddress { set; get; }

        public AddressResponse Address { set; get; }

        public string PhoneNumber { set; get; }
    }

    public class AddressResponse
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }
}
