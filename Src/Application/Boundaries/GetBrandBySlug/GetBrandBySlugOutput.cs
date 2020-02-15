using TreniniDotNet.Domain.Catalog.Brands;

namespace TreniniDotNet.Application.Boundaries.GetBrandBySlug
{
    public class GetBrandBySlugOutput : IUseCaseOutput
    {
        private IBrand? _brand;

        public GetBrandBySlugOutput(IBrand? brand
            )
        {
            _brand = brand;
        }

        public IBrand? Brand => _brand;
    }
}
