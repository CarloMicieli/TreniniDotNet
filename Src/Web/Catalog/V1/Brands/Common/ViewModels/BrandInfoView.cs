using System;
using TreniniDotNet.Domain.Catalog.Brands;

namespace TreniniDotNet.Web.Catalog.V1.Brands.Common.ViewModels
{
    public sealed class BrandInfoView
    {
        private readonly Brand _brand;

        public BrandInfoView(Brand brand)
        {
            _brand = brand ??
                throw new ArgumentNullException(nameof(brand));
        }

        public Guid Id => _brand.Id;

        public string Name => _brand.Name;

        public string Slug => _brand.Slug.ToString();
    }
}
