namespace Common.Addresses
{
    public sealed class Address
    {
        public Address(string line1, string? line2, string city, string? region, string postalCode, string country)
        {
            Line1 = line1;
            Line2 = line2;
            City = city;
            Region = region;
            PostalCode = postalCode;
            Country = country;
        }

        public string Line1 { get; }
        public string? Line2 { get; }
        public string City { get; }

        // State / Province / Region
        public string? Region { get; }

        // ZIP/Postal Code
        public string PostalCode { get; }
        public string Country { get; }
    }
}