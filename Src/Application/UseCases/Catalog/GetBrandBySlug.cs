using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.GetBrandBySlug;
using TreniniDotNet.Domain.Catalog.Brands;

namespace TreniniDotNet.Application.UseCases.Catalog
{
    public class GetBrandBySlug : IUseCase
    {
        private readonly IOutputPort _outputPort;
        private readonly BrandService _brandService;

        public GetBrandBySlug(IOutputPort outputPort, BrandService brandService)
        {
            _outputPort = outputPort;
            _brandService = brandService;
        }

        public async Task Execute(GetBrandBySlugInput input)
        {
            try
            {
                var brand = await _brandService.GetBy(input.Slug);
                _outputPort.Standard(new GetBrandBySlugOutput(brand));
            }
            catch (BrandNotFoundException)
            {
                _outputPort.BrandNotFound("not found");
            }
        }
    }
}
