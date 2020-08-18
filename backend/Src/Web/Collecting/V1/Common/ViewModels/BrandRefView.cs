using TreniniDotNet.Domain.Catalog.CatalogItems;

namespace TreniniDotNet.Web.Collecting.V1.Common.ViewModels
{
    public sealed class BrandRefView
    {
        private readonly BrandRef _brand;

        public BrandRefView(BrandRef brand)
        {
            _brand = brand;
        }

        public string Slug => _brand.Slug;
        public string Value => _brand.ToString();
    }
}
