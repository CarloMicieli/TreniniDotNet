using TreniniDotNet.Application.Boundaries.Common;
using TreniniDotNet.Common.Interfaces;

namespace TreniniDotNet.Application.Boundaries.Collection.CreateShop
{
    public sealed class CreateShopInput : IUseCaseInput
    {
        public CreateShopInput(string name, string? websiteUrl, string? emailAddress, AddressInput? address, string? phoneNumber)
        {
            Name = name;
            WebsiteUrl = websiteUrl;
            Address = address;
            PhoneNumber = phoneNumber;
            EmailAddress = emailAddress;
        }

        public string Name { get; }

        public string? WebsiteUrl { get; }

        public AddressInput? Address { get; }

        public string? EmailAddress { get; }

        public string? PhoneNumber { get; }
    }
}
