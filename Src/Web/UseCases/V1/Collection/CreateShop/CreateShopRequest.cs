using MediatR;
using TreniniDotNet.Web.UseCases.V1.Common;

namespace TreniniDotNet.Web.UseCases.V1.Collection.CreateShop
{
    public sealed class CreateShopRequest : IRequest
    {
        public string? Name { get; set; }

        public string? WebsiteUrl { get; set; }

        public AddressRequest? Address { get; set; } = new AddressRequest();

        public string? EmailAddress { get; set; }

        public string? PhoneNumber { get; set; }
    }
}
