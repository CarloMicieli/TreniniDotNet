using TreniniDotNet.SharedKernel.Addresses;

namespace TreniniDotNet.Web.Collecting.V1.Shops.Common.ViewModels
{
    public class AddressView
    {
        public AddressView(Address address)
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
