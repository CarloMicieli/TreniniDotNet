using MediatR;
using TreniniDotNet.Web.UseCases.V1.Common;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.EditBrand
{
    public sealed class EditBrandRequest : IRequest
    {
        public string? Name { get; set; }
        public string? Slug { get; set; }
        public string? CompanyName { get; set; }
        public string? GroupName { get; set; }
        public string? Description { get; set; }
        public string? WebsiteUrl { get; set; }
        public string? EmailAddress { get; set; }
        public string? BrandType { get; set; }
        public AddressRequest? Address { get; set; }
    }
}
