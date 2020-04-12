using System;
using TreniniDotNet.Domain.Catalog.Brands;

namespace TreniniDotNet.Web.ViewModels.V1.Catalog
{
    public sealed class BrandInfoView
    {
        private readonly IBrandInfo _brand;

        public BrandInfoView(IBrandInfo brand)
        {
            _brand = brand ??
                throw new ArgumentNullException(nameof(brand));
        }

        public Guid Id => _brand.BrandId.ToGuid();

        public string Name => _brand.Name;

        public string Slug => _brand.Slug.ToString();
    }
}
