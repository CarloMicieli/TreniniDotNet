using MediatR;
using Newtonsoft.Json;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.Web.Catalog.V1.Brands.Common.Requests;

namespace TreniniDotNet.Web.Catalog.V1.Brands.EditBrand
{
    public sealed class EditBrandRequest : IRequest
    {
        [JsonIgnore]
        public Slug? BrandSlug { get; set; }
        public string? Name { get; set; }
        public string? CompanyName { get; set; }
        public string? GroupName { get; set; }
        public string? Description { get; set; }
        public string? WebsiteUrl { get; set; }
        public string? EmailAddress { get; set; }
        public string? BrandType { get; set; }
        public AddressRequest? Address { get; set; }
    }
}
