using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Collecting.Shared;

namespace TreniniDotNet.Web.Collecting.V1.Common.ViewModels
{
    public sealed class BrandRefView
    {
        private readonly Brand _brand;

        public BrandRefView(Brand brand)
        {
            _brand = brand;
        }

        public string Slug => _brand.Slug;
        public string Value => _brand.Name;
    }
}
