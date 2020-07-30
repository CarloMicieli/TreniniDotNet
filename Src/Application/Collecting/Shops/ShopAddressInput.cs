using TreniniDotNet.SharedKernel.Addresses;

namespace TreniniDotNet.Application.Collecting.Shops
{
    public sealed class ShopAddressInput
    {
        public string? Line1 { set; get; }
        public string? Line2 { set; get; }
        public string? City { set; get; }
        public string? Region { set; get; }
        public string? PostalCode { set; get; }
        public string? Country { set; get; }

        public Address? ToDomainAddress()
        {
            return Address.TryCreate(
                Line1,
                Line2,
                City,
                Region,
                PostalCode,
                Country,
                out var address) ? address : null;
        }
    }
}
