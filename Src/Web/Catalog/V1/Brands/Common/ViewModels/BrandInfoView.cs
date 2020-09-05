using System;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems;

namespace TreniniDotNet.Web.Catalog.V1.Brands.Common.ViewModels
{
    public sealed class BrandInfoView
    {
        private readonly BrandRef _brand;

        public BrandInfoView(BrandRef brand)
        {
            _brand = brand ??
                throw new ArgumentNullException(nameof(brand));
        }

        public Guid Id => _brand.Id;

        public string Name => _brand.ToString();

        public string Slug => _brand.Slug.ToString();
    }
}
