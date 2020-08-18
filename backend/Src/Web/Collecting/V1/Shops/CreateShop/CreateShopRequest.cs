using MediatR;
using TreniniDotNet.Web.Collecting.V1.Shops.Common.Requests;

namespace TreniniDotNet.Web.Collecting.V1.Shops.CreateShop
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
