using TreniniDotNet.Application.Boundaries.Common;

namespace TreniniDotNet.Application.TestInputs.Common
{
    public static class CommonInputs
    {
        public static class NewAddressInput
        {
            public static AddressInput NewEmpty() => With();

            public static AddressInput With(
                string Line1 = null,
                string Line2 = null,
                string City = null,
                string Region = null,
                string PostalCode = null,
                string Country = null) => new AddressInput
                {
                    Line1 = Line1,
                    Line2 = Line2,
                    City = City,
                    Region = Region,
                    PostalCode = PostalCode,
                    Country = Country
                };
        }
    }
}
