using MediatR;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.CreateBrand
{
    public sealed class CreateBrandRequest : IRequest
    {
        public string Name { set; get; } = "";
        public string? CompanyName { set; get; }
        public string? WebsiteUrl { set; get; }
        public string? EmailAddress { set; get; }
        public string? BrandType { set; get; }
    }
}
