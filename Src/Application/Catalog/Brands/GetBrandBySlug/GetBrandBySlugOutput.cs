using TreniniDotNet.Common.UseCases.Interfaces.Output;
using TreniniDotNet.Domain.Catalog.Brands;

namespace TreniniDotNet.Application.Catalog.Brands.GetBrandBySlug
{
    public class GetBrandBySlugOutput : IUseCaseOutput
    {
        private IBrand _brand;

        public GetBrandBySlugOutput(IBrand brand)
        {
            _brand = brand;
        }

        public IBrand Brand => _brand;
    }
}
