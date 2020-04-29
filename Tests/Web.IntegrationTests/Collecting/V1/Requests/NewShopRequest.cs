namespace TreniniDotNet.IntegrationTests.Collecting.V1.Requests
{
    public sealed class NewShopRequest
    {
        public string Name { get; set; }
        public string WebsiteUrl { get; set; }
        public ShopAddressRequest Address { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
    }

    public sealed class ShopAddressRequest
    {
        public string Line1 { set; get; }
        public string Line2 { set; get; }
        public string City { set; get; }
        public string Region { set; get; }
        public string PostalCode { set; get; }
        public string Country { set; get; }
    }
}