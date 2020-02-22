using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Common;

namespace TreniniDotNet.Application.Boundaries.Catalog.CreateBrand
{
    public sealed class CreateBrandOutput : IUseCaseOutput
    {
        private readonly Slug _slug;

        public CreateBrandOutput(IBrand brand)
        {
            _slug = brand.Slug;
        }

        public Slug Slug => _slug;
    }
}
