using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;

namespace TreniniDotNet.Application.Boundaries.Catalog.CreateBrand
{
    public sealed class CreateBrandOutput : IUseCaseOutput
    {
        private readonly BrandId _brandId;
        private readonly Slug _slug;

        public CreateBrandOutput(IBrand brand)
        {
            _brandId = brand.BrandId;
            _slug = brand.Slug;
        }

        public BrandId BrandId => _brandId;

        public Slug Slug => _slug;
    }
}
