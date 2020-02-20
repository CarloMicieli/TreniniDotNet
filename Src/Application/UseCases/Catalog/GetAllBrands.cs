using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.GetAllBrands;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Domain.Catalog.Brands;

namespace TreniniDotNet.Application.UseCases.Catalog
{
    public sealed class GetAllBrands : IGetAllBrandsUseCase
    {
        private readonly IOutputPort _outputPort;
        private readonly IUnitOfWork _unitOfWork;
        private readonly BrandService _brandService;

        public GetAllBrands(IOutputPort outputPort, IUnitOfWork unitOfWork, BrandService brandService)
        {
            _outputPort = outputPort;
            _unitOfWork = unitOfWork;
            _brandService = brandService;
        }

        public Task Execute(GetAllBrandsInput input)
        {
            throw new System.NotImplementedException("TODO");
        }
    }
}
