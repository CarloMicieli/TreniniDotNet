using System;
using System.Threading.Tasks;
using TreniniDotNet.Domain.Catalog.Brands;

namespace TreniniDotNet.Application.Catalog.Brands.GetBrandsList
{
    public sealed class GetBrandsListUseCase : IGetBrandsListUseCase
    {
        private readonly BrandService _brandService;

        public GetBrandsListUseCase(IGetBrandsListOutputPort outputPort, BrandService brandService)
        {
            OutputPort = outputPort ??
                throw new ArgumentNullException(nameof(outputPort));
            _brandService = brandService ??
                throw new ArgumentNullException(nameof(brandService));
        }

        public async Task Execute(GetBrandsListInput input)
        {
            var paginatedResult = await _brandService.FindAllBrands(input.Page);
            OutputPort.Standard(new GetBrandsListOutput(paginatedResult));
        }

        private IGetBrandsListOutputPort OutputPort { get; }
    }
}
