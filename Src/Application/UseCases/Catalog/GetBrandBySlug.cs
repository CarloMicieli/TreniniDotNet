using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.GetBrandBySlug;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Domain.Catalog.Brands;

namespace TreniniDotNet.Application.UseCases.Catalog
{
    public class GetBrandBySlug : IGetBrandBySlugUseCase
    {
        private readonly IOutputPort _outputPort;
        private readonly IUnitOfWork _unitOfWork;
        private readonly BrandService _brandService;

        public GetBrandBySlug(IOutputPort outputPort, IUnitOfWork unitOfWork, BrandService brandService)
        {
            _outputPort = outputPort;
            _unitOfWork = unitOfWork;
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
