using TreniniDotNet.Common.UseCases.Boundaries.Outputs;
using TreniniDotNet.Domain.Catalog.Brands;

namespace TreniniDotNet.Application.Catalog.Brands.GetBrandBySlug
{
    public sealed class GetBrandBySlugOutput : IUseCaseOutput
    {
        public GetBrandBySlugOutput(Brand brand)
        {
            Brand = brand;
        }

        public Brand Brand { get; }
    }
}
