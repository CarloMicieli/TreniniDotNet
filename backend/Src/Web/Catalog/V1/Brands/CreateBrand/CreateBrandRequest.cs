using MediatR;
using TreniniDotNet.Web.Catalog.V1.Brands.Common.Requests;

namespace TreniniDotNet.Web.Catalog.V1.Brands.CreateBrand
{
    public sealed class CreateBrandRequest : IRequest
    {
        public string Name { set; get; } = "";
        public string? CompanyName { set; get; }
        public string? GroupName { set; get; }
        public string? Description { set; get; }
        public string? WebsiteUrl { set; get; }
        public string? EmailAddress { set; get; }
        public string? BrandType { set; get; }
        public AddressRequest? Address { set; get; } = new AddressRequest();
    }
}
