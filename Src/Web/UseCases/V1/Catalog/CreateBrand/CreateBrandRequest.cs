using MediatR;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.CreateBrand
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
        public AddressRequest? Address { set; get; }
    }

    public sealed class AddressRequest
    {
        public string? Line1 { set; get; }
        public string? Line2 { set; get; }
        public string? City { set; get; }
        public string? Region { set; get; }
        public string? PostalCode { set; get; }
        public string? Country { set; get; }
    }
}
